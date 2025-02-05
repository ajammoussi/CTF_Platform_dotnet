using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.Models.Enums;
using CTF_Platform_dotnet.Repositories;
using CTF_Platform_dotnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CTF_Platform_dotnet.DTOs;

namespace CTF_Platform_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly CTFContext _context;
        private readonly IUserService _userService;
        private readonly IRepository<Submission> _submissionRepository;
        public UserController(CTFContext context, IUserService userService, IRepository<Submission> submissionRepository)
        {
            _context = context;
            _userService = userService;
            _submissionRepository = submissionRepository;
        }


        [Authorize]
        [HttpGet("GetUser")]
        public IActionResult GetUser(int  id)
        {
            var user = new
            {
                Id = id,
                Name = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Role = User.FindFirst(ClaimTypes.Role)?.Value
            };
            return Ok(user);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("getadmin")]
        public IActionResult GetAdmin()
        {
            return Ok(new { Message = "This is an admin-only endpoint" });
        }

        [Authorize(Policy = "ParticipantOnly")]
        [HttpGet("getparticipant")]
        public IActionResult GetParticipant()
        {
            return Ok(new { Message = "This is a participant-only endpoint" });
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

        [Authorize(Policy = "ParticipantOrAdmin")]
        [HttpGet("currentuser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID claim not found.");
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Invalid User ID claim.");
            }

            // Fetch the user from the database
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpGet("correctSubmissions")]
        [Authorize(Policy = "ParticipantOnly")]
        public async Task<IActionResult> GetCorrectSubmissions()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var correctSubmissions = _submissionRepository
                .Where(s => s.UserId == user.UserId && s.IsCorrect)
                .Select(s => new CorrectSubmissionDto
                {
                    SubmissionId = s.SubmissionId,
                    ChallengeId = s.ChallengeId,
                    ChallengeName = s.Challenge.Name,
                    UserId = s.UserId,
                    TeamId = s.TeamId,
                    SubmittedFlag = s.SubmittedFlag,
                    SubmittedAt = s.SubmittedAt,
                    IsCorrect = s.IsCorrect
                })
                .ToList();

            return Ok(correctSubmissions);
        }
    }
}
