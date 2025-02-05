using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.DTOs;
using CTF_Platform_dotnet.Repositories;
using CTF_Platform_dotnet.Services;
using Microsoft.AspNetCore.Authorization;

namespace CTF_Platform_dotnet.Controllers
{
    // This endpoint is reachable at: POST /api/challenges/solve
    [Route("api/challenges/solve")]
    [ApiController]
    public class SolveController : ControllerBase
    {
        private readonly IRepository<Challenge> _challengeRepository;
        private readonly IRepository<Submission> _submissionRepository;
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public SolveController(
            IRepository<Challenge> challengeRepository,
            IRepository<Submission> submissionRepository,
            IMapper mapper,
            IUserService userService,
            ITeamService teamService)
        {
            _challengeRepository = challengeRepository;
            _submissionRepository = submissionRepository;
            _mapper = mapper;
            _userService = userService;
            _teamService = teamService;
        }

        [HttpPost]
        [Authorize(Policy = "ParticipantOnly")]
        public async Task<IActionResult> SubmitSolution([FromBody] SubmissionDto submissionDto)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // Retrieve the challenge by ID
            var challenge = await _challengeRepository.GetByIdAsync(submissionDto.ChallengeId);
            if (challenge == null)
            {
                return NotFound("Challenge not found.");
            }

            // Redundancy check: Ensure the team hasn’t already solved the challenge
            var existingSubmission = _submissionRepository
                .Where(s => s.ChallengeId == submissionDto.ChallengeId
                          && s.TeamId == user.TeamId
                          && s.IsCorrect == true)
                .FirstOrDefault();

            if (existingSubmission != null)
            {
                return BadRequest("Challenge already solved by the team.");
            }

            // Check if the submitted flag matches the correct flag
            bool isCorrect = submissionDto.SubmittedFlag == challenge.Flag;

            if (isCorrect)
            {
                // Update the user's score
                user.Points += challenge.Points;
                await _userService.UpdateUserAsync(user);

                // Update the team's score
                var team = await _teamService.GetTeamByIdAsync(user.TeamId.GetValueOrDefault());
                team.TotalPoints += challenge.Points;
                await _teamService.UpdateTeamAsync(team);
            }

            // Map the Submission Details to a Submission entity
            var submission = _mapper.Map<Submission>(submissionDto);
            submission.UserId = user.UserId;
            submission.TeamId = user.TeamId ?? throw new InvalidOperationException("User's TeamId is null.");
            submission.SubmittedAt = System.DateTime.UtcNow;
            submission.IsCorrect = isCorrect;

            // Save the new submission
            await _submissionRepository.AddAsync(submission);

            // Return a simple response indicating if the solution was correct
            return Ok(new
            {
                message = isCorrect ? "Correct" : "Incorrect",
                submissionId = submission.SubmissionId
            });
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
