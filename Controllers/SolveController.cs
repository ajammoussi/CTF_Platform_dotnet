using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.DTOs;
using CTF_Platform_dotnet.Repositories;

namespace CTF_Platform_dotnet.Controllers
{
    // This endpoint is reachable at: POST /api/challenges/{challengeId}/solve
    [Route("api/challenges/{challengeId}/solve")]
    [ApiController]
    public class SolveController : ControllerBase
    {
        private readonly IRepository<Challenge> _challengeRepository;
        private readonly IRepository<Submission> _submissionRepository;
        private readonly IMapper _mapper;

        public SolveController(
            IRepository<Challenge> challengeRepository,
            IRepository<Submission> submissionRepository,
            IMapper mapper)
        {
            _challengeRepository = challengeRepository;
            _submissionRepository = submissionRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitSolution(int challengeId, [FromBody] SubmissionDto submissionDto)
        {
            // TODO: In production, set UserId and TeamId from the authenticated context.
            // For now, ensure TeamId is provided in the DTO.
            if (submissionDto.TeamId == null)
            {
                return BadRequest("TeamId is required.");
            }

            // Retrieve the challenge by ID
            var challenge = await _challengeRepository.GetByIdAsync(challengeId);
            if (challenge == null)
            {
                return NotFound("Challenge not found.");
            }

            // Redundancy check: Ensure the team hasn’t already solved the challenge
            var existingSubmission = _submissionRepository
                .Where(s => s.ChallengeId == challengeId
                          && s.TeamId == submissionDto.TeamId
                          && s.IsCorrect == true)
                .FirstOrDefault();

            if (existingSubmission != null)
            {
                return BadRequest("Challenge already solved by the team.");
            }

            // Check if the submitted flag matches the correct flag
            bool isCorrect = submissionDto.SubmittedFlag == challenge.Flag;
            submissionDto.IsCorrect = isCorrect;

            // Map the SubmissionDto to a Submission entity
            var submission = _mapper.Map<Submission>(submissionDto);
            submission.ChallengeId = challengeId;
            submission.SubmittedAt = System.DateTime.UtcNow;

            // Save the new submission
            await _submissionRepository.AddAsync(submission);

            // Return a simple response indicating if the solution was correct
            return Ok(new
            {
                message = isCorrect ? "Correct" : "Incorrect",
                submissionId = submission.SubmissionId
            });
        }
    }
}
