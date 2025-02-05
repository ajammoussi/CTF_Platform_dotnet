using CTF_Platform_dotnet.DTOs;
using CTF_Platform_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using CTF_Platform_dotnet.Repositories;
using AutoMapper;

namespace CTF_Platform_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengesController : ControllerBase
    {
        private readonly IRepository<Challenge> _challengeRepository;
        private readonly IMapper _mapper;

        public ChallengesController(
            IRepository<Challenge> challengeRepository,
            IMapper mapper)
        {
            _challengeRepository = challengeRepository;
            _mapper = mapper;
        }

        // GET: api/challenges
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var pagedChallenges = await _challengeRepository.GetPagedAsync(pageNumber, pageSize);
            var challenges = _mapper.Map<List<ChallengeDto>>(pagedChallenges.Items);

            // Use PagedResponse<T> instead of PagedResult<T>
            return Ok(new PagedResponse<ChallengeDto>(
                challenges,
                pageNumber,
                pageSize,
                pagedChallenges.TotalItems
            ));
        }

        // GET: api/challenges/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var challenge = await _challengeRepository.GetByIdAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ChallengeDto>(challenge));
        }

        // POST: api/challenges
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateChallengeDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var challenge = _mapper.Map<Challenge>(createDto);
            await _challengeRepository.AddAsync(challenge);

            return CreatedAtAction(nameof(GetById),
                new { id = challenge.ChallengeId },
                _mapper.Map<ChallengeDto>(challenge));
        }

        // PUT: api/challenges/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateChallengeDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var challenge = await _challengeRepository.GetByIdAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }

            _mapper.Map(updateDto, challenge);
            await _challengeRepository.UpdateAsync(challenge);
            return NoContent();
        }

        // DELETE: api/challenges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var challenge = await _challengeRepository.GetByIdAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }

            await _challengeRepository.DeleteAsync(challenge);

            return NoContent();
        }
    }
}