using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.Services;
using CTF_Platform_dotnet.Services.EmailSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static CTF_Platform_dotnet.Services.Generic.IService;

namespace CTF_Platform_dotnet.Controllers
{

    public class InvitationRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }

    public class CreateTeamRequest
    {
        [Required, MaxLength(100)]
        public string TeamName { get; set; }
    }

    [Route("api/team")]
    [ApiController]
    public class TeamController : Controller
    {
        private readonly CTFContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;
        private readonly IService<Invitation> _invitationService;

        public TeamController(CTFContext context, IEmailSender emailSender, IUserService userService, ITeamService teamService, IService<Invitation> invitationService)
        {
            _context = context;
            _emailSender = emailSender;
            _userService = userService;
            _teamService = teamService;
            _invitationService = invitationService;
        }

        [Authorize(Policy = "ParticipantOnly")]
        [HttpPost("create-team")]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamRequest request)
        {
            var user = await GetCurrentUserAsync();
            var oldTeam = (await _teamService.GetTeamsByPredicateAsync(t => t.CreatedByUserId == user.UserId)).FirstOrDefault();

            if (oldTeam == null)
            {
                return BadRequest("User is not part of an existing team.");
            }

            // Create the new team
            var newTeam = new Team
            {
                TeamName = request.TeamName,
                CreatedByUserId = user.UserId,
                TotalPoints = user.Points,
                CreatedAt = DateTime.UtcNow,
                CreatedByUser = user,
                Users = new List<User> { user }
            };

            // Save the new team to the database first
            _context.Teams.Add(newTeam);
            await _context.SaveChangesAsync(); // This generates the TeamId

            // Update the user's TeamId to the new team's ID
            user.TeamId = newTeam.TeamId;

            // Deduct points from the old team
            oldTeam.TotalPoints -= user.Points;

            // remove oldTeam from the db
            _context.Teams.Remove(oldTeam);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(newTeam);
        }

        [Authorize(Policy = "ParticipantOnly")]
        [HttpPost("create-invitation/{id}")]
        public async Task<IActionResult> CreateInvitation(string id)
        {
            // Fetch the current user
            var currentUser = await GetCurrentUserAsync();
            Console.WriteLine(currentUser.UserId);
            var team = await _context.Teams.FindAsync(currentUser.TeamId);

            if (team == null)
                return NotFound("Team not found");

            // Convert the id parameter to an int
            if (!int.TryParse(id, out int userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            // Fetch the user to whom the invitation is being sent
            var userToInvite = await _context.Users.FindAsync(userId); // <-- Now using int
            if (userToInvite == null)
                return NotFound("User not found");

            // Create the invitation
            var invitation = new Invitation
            {
                Token = Guid.NewGuid().ToString(),
                Expiration = DateTime.UtcNow.AddMinutes(5),
                TeamId = team.TeamId,
                UserEmail = userToInvite.Email // Use the email of the user to be invited
            };

            _context.Invitations.Add(invitation);
            await _context.SaveChangesAsync();

            // Generate the callback URL
            var callbackUrl = Url.Action("AcceptInvitation", "Team",
                new { token = invitation.Token }, Request.Scheme);

            // Send the invitation email
            await _emailSender.SendEmailAsync(
                userToInvite.Email,
                "Team Invitation",
                $"Join the team by clicking here: {callbackUrl}");

            return Ok(new { message = "Invitation sent" });
        }

        [Authorize(Policy = "ParticipantOnly")]
        [HttpPost("accept-invitation/{token}")]
        public async Task<IActionResult> AcceptInvitation(string token)
        {
            Console.WriteLine("Starting AcceptInvitation method...");

            // Fetch the current user
            var user = await GetCurrentUserAsync();
            Console.WriteLine($"Current user: UserId = {user.UserId}, TeamId = {user.TeamId}, Points = {user.Points}");

            // Fetch the invitation by token
            var invitation = (await _invitationService.GetByPredicateAsync(i => i.Token == token)).FirstOrDefault();
            Console.WriteLine($"Invitation found: {(invitation != null ? $"Token = {invitation.Token}, TeamId = {invitation.TeamId}" : "null")}");

            if (invitation == null)
            {
                Console.WriteLine("Invalid or expired invitation.");
                return BadRequest("Invalid or expired invitation");
            }

            // Fetch the user's current team (old team)
            var oldTeam = (await _teamService.GetTeamsByPredicateAsync(t => t.TeamId == user.TeamId)).FirstOrDefault();
            Console.WriteLine($"Old team: {(oldTeam != null ? $"TeamId = {oldTeam.TeamId}, TotalPoints = {oldTeam.TotalPoints}" : "null")}");

            // Fetch the new team (team from the invitation)
            var newTeam = await _teamService.GetTeamByIdAsync(invitation.TeamId);
            Console.WriteLine($"New team: TeamId = {newTeam.TeamId}, TotalPoints = {newTeam.TotalPoints}");

            if (oldTeam != null)
            {
                // Deduct the user's points from the old team
                oldTeam.TotalPoints -= user.Points;
                Console.WriteLine($"Old team updated: TotalPoints = {oldTeam.TotalPoints}");
            }

            // Update the user's team and points
            user.TeamId = newTeam.TeamId;
            user.Points = 0;
            Console.WriteLine($"User updated: TeamId = {user.TeamId}, Points = {user.Points}");

            // Add the user's points to the new team
            newTeam.TotalPoints += user.Points;
            Console.WriteLine($"New team updated: TotalPoints = {newTeam.TotalPoints}");

            // Save changes to the database
            await _context.SaveChangesAsync();
            Console.WriteLine("Changes saved to the database.");

            return Ok("Changed to new team");
        }

        [Authorize(Policy = "ParticipantOnly")]
        [HttpPost("leave-team")]
        public async Task<IActionResult> LeaveTeam()
        {
            var user = await GetCurrentUserAsync();
            var team = (await _teamService.GetTeamsByPredicateAsync(t => t.TeamId == user.TeamId)).FirstOrDefault();

            if (team == null)
                return NotFound("Team not found");

            // Check if the current user is the team creator
            if (user.UserId == team.CreatedByUserId)
            {
                return BadRequest("The team creator cannot leave the team. Please delete the team instead.");
            }

            if (user.UserId == team.CreatedByUserId)
            {
                foreach (var member in team.Users.ToList())
                {
                    await CreatePersonalTeam(member);
                }
                _context.Teams.Remove(team);
            }
            else
            {
                team.TotalPoints -= user.Points;
                await CreatePersonalTeam(user);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private async Task CreatePersonalTeam(User user)
        {
            var personalTeam = new Team
            {
                TeamName = user.Username,
                CreatedByUserId = user.UserId,
                TotalPoints = user.Points,
                CreatedAt = DateTime.UtcNow,
                CreatedByUser = user,
                Users = new List<User> { user }
            };

            _context.Teams.Add(personalTeam);
            await _context.SaveChangesAsync();
            user.TeamId = personalTeam.TeamId;
        }

        private async Task<User> GetCurrentUserAsync()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new UnauthorizedAccessException("User ID claim not found.");
            }

            int userId = int.Parse(userIdClaim); // Convert to int

            // Fetch the user from the database
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            return user;
        }

    }
}
