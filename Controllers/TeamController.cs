using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.Services.EmailSender;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
[Route("team")]
[ApiController]
public class TeamController : Controller
{
    private readonly CTFContext _context;
    private readonly IEmailSender _emailSender;

    public TeamController(CTFContext context, IEmailSender emailSender)
    {
        _context = context;
        _emailSender = emailSender;
    }

    [HttpPost("create-team")]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamRequest request)
    {
        var user = await GetCurrentUser();
        var oldTeam = await _context.Teams.FindAsync(user.TeamId);

        if (oldTeam == null)
        {
            return BadRequest("User is not part of an existing team.");
        }

        var newTeam = new Team
        {
            TeamName = request.TeamName,
            CreatedByUserId = user.UserId,
            TotalPoints = user.Points,
            CreatedAt = DateTime.UtcNow,
            CreatedByUser = user,
            Users = new List<User> { user }
        };

        oldTeam.TotalPoints -= user.Points;
        _context.Teams.Add(newTeam);
        user.TeamId = newTeam.TeamId;

        await _context.SaveChangesAsync();

        return Ok(newTeam);
    }

    [HttpPost("create-invitation")]
    public async Task<IActionResult> CreateInvitation([FromBody] InvitationRequest request)
    {
        var currentUser = await GetCurrentUser();
        var team = await _context.Teams.FindAsync(currentUser.TeamId);

        if (team == null)
            return NotFound("Team not found");

        var invitation = new Invitation
        {
            Token = Guid.NewGuid().ToString(),
            Expiration = DateTime.UtcNow.AddMinutes(5),
            TeamId = team.TeamId,
            UserEmail = request.Email
        };

        _context.Invitations.Add(invitation);
        await _context.SaveChangesAsync();

        var callbackUrl = Url.Action("AcceptInvitation", "Team",
            new { token = invitation.Token }, Request.Scheme);

        await _emailSender.SendEmailAsync(
            request.Email,
            "Team Invitation",
            $"Join the team by clicking here: {callbackUrl}");

        return Ok(new { message = "Invitation sent" });
    }

    [HttpGet("accept-invitation/{token}")]
    public async Task<IActionResult> AcceptInvitation(string token)
    {
        var user = await GetCurrentUser();
        var invitation = await _context.Invitations
            .FirstOrDefaultAsync(i => i.Token == token && i.Expiration > DateTime.UtcNow);

        if (invitation == null)
            return BadRequest("Invalid or expired invitation");

        var oldTeam = await _context.Teams.FindAsync(user.TeamId);
        var newTeam = await _context.Teams.FindAsync(invitation.TeamId);

        if (oldTeam != null)
        {
            oldTeam.TotalPoints -= user.Points;
        }

        user.TeamId = newTeam.TeamId;
        user.Points = 0;
        newTeam.TotalPoints += user.Points;

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("leave-team")]
    public async Task<IActionResult> LeaveTeam()
    {
        var user = await GetCurrentUser();
        var team = await _context.Teams
            .Include(t => t.Users)
            .FirstOrDefaultAsync(t => t.TeamId == user.TeamId);

        if (team == null)
            return NotFound("Team not found");

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

    [HttpPost("kick-member/{userId}")]
    public async Task<IActionResult> KickMember(int userId)
    {
        var currentUser = await GetCurrentUser();
        var team = await _context.Teams.FindAsync(currentUser.TeamId);

        if (team?.CreatedByUserId != currentUser.UserId)
            return Forbid();

        var userToKick = await _context.Users.FindAsync(userId);
        if (userToKick == null || userToKick.TeamId != team.TeamId)
            return NotFound("User not in team");

        team.TotalPoints -= userToKick.Points;
        await CreatePersonalTeam(userToKick);

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

    private async Task<User> GetCurrentUser()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return await _context.Users.FindAsync(userId);
    }

    [HttpPost("test-email")]
    public async Task<IActionResult> TestEmail([FromQuery] string email)
    {
        try
        {
            await _emailSender.SendEmailAsync(
                email,
                "Test Email",
                "<strong>This is a test email from CTF Platform</strong>"
            );
            return Ok("Email sent successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error sending email: {ex.Message}");
        }
    }
}
