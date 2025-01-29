using Microsoft.AspNetCore.Mvc;
using CTF_Platform_dotnet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using CTF_Platform_dotnet.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using CTF_Platform_dotnet.DTOs;

namespace CTF_Platform_dotnet.Controllers{
    [Route("admin")]
    [ApiController]
    public class AdminController : ControllerBase 
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Challenge> _challengeRepository;
        private readonly IRepository<Submission> _submissionRepository;
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<SupportTicket> _supportTicketRepository;
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;



        public AdminController(
            IRepository<User> userRepository, 
            IRepository<Challenge> challengeRepository, 
            IRepository<Submission> submissionRepository, 
            IRepository<Team> teamRepository, 
            IRepository<SupportTicket> supportTicketRepository,
            IAdminService adminService,
            ILogger<AdminController> logger)
        {
            _userRepository = userRepository;
            _challengeRepository = challengeRepository;
            _submissionRepository = submissionRepository;
            _teamRepository = teamRepository;
            _supportTicketRepository = supportTicketRepository;
            _adminService = adminService;
            _logger = logger;
        }
        [HttpGet]
        [Route("")]
        [Route("dashboard")]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Fetching admin dashboard statistics...");
            try{
                var (mostSolved, leastSolved) = await _adminService.GetMostAndLeastSolvedChallengesAsync();
                var statistics = new {
                    TotalUsers = await _userRepository.CountAsync(),
                    TotalTeams = await _teamRepository.CountAsync(),
                    TotalChallenges = await _challengeRepository.CountAsync(),
                    TotalSubmissions = await _submissionRepository.CountAsync(),
                    TotalSupportTickets = await _supportTicketRepository.CountAsync(),
                    MostSolvedChallenge = mostSolved,
                    LeastSolvedChallenge = leastSolved
                };
                _logger.LogInformation("Successfully fetched admin statistics.");
                return Ok(statistics); 
            
            }catch (Exception ex){
                _logger.LogError(ex, "Error fetching admin dashboard statistics.");
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> Users([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            _logger.LogInformation("Fetching users: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
            try {
                if (pageNumber <= 0) 
                {
                    _logger.LogWarning("Invalid page number: {PageNumber}", pageNumber);
                    return BadRequest("Page number must be greater than 0.");
                }
                if (pageSize <= 0 || pageSize > 50) 
                {
                    _logger.LogWarning("Invalid page size: {PageSize}", pageSize); 
                    return BadRequest("Page size must be between 1 and 50.");
                }

                var pagedUsers  = await _userRepository.GetPagedAsync(pageNumber, pageSize);

                //using DTO to return only necessary fields (avoiding sensitive data)
                var users = pagedUsers.Items.Select(u => new UserDto{
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt,
                    Points = u.Points,
                    TeamId = u.TeamId,
                    TeamName = u.Team.TeamName,
                });
                var pagedResult = new PagedResponse<UserDto>
                (
                    users.ToList(),
                    pageNumber,
                    pageSize,
                    pagedUsers.TotalItems
                );

                 _logger.LogInformation("Successfully fetched users");
                return Ok(pagedResult);
                
            } catch (Exception ex) {
                _logger.LogError(ex, "Error fetching users.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("challenges")]
        public async Task<IActionResult> Challenges([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            _logger.LogInformation("Fetching challenges: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
            try {
                if (pageNumber <= 0) 
                {
                    _logger.LogWarning("Invalid page number: {PageNumber}", pageNumber);
                    return BadRequest("Page number must be greater than 0.");
                }
                if (pageSize <= 0 || pageSize > 50) 
                {
                    _logger.LogWarning("Invalid page size: {PageSize}", pageSize); 
                    return BadRequest("Page size must be between 1 and 50.");
                }

                var pagedChallenges  = await _challengeRepository.GetPagedAsync(pageNumber, pageSize);
                _logger.LogInformation("Successfully fetched challenges");
                return Ok(pagedChallenges);
            } catch (Exception ex) {
                _logger.LogError(ex, "Error fetching challenges.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("submissions")]
        public async Task<IActionResult> Submissions ([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            _logger.LogInformation("Fetching submissions: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
            try {
                if (pageNumber <= 0) 
                {
                    _logger.LogWarning("Invalid page number: {PageNumber}", pageNumber);
                    return BadRequest("Page number must be greater than 0.");
                }
                if (pageSize <= 0 || pageSize > 50) 
                {
                    _logger.LogWarning("Invalid page size: {PageSize}", pageSize); 
                    return BadRequest("Page size must be between 1 and 50.");
                }

                var pagedSubmissions  = await _submissionRepository.GetPagedAsync(pageNumber, pageSize);

                _logger.LogInformation("Successfully fetched submissions");
                return Ok(pagedSubmissions);
            } catch (Exception ex) {
                _logger.LogError(ex, "Error fetching submissions.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("scoreboard/teams")]
        public async Task<IActionResult> ScoreboardTeam ([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            _logger.LogInformation("Fetching team scoreboard: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
            try {
                if (pageNumber <= 0) 
                {
                    _logger.LogWarning("Invalid page number: {PageNumber}", pageNumber);
                    return BadRequest("Page number must be greater than 0.");
                }
                if (pageSize <= 0 || pageSize > 50) 
                {
                    _logger.LogWarning("Invalid page size: {PageSize}", pageSize); 
                    return BadRequest("Page size must be between 1 and 50.");
                }

                var pagedTeamScoreboard = await _adminService.GetTeamScoreboardAsync(pageNumber, pageSize);
                
                _logger.LogInformation("Successfully fetched team scoreboard");
                return Ok(pagedTeamScoreboard);

            } catch (Exception ex) {
                _logger.LogError(ex, "Error fetching team scoreboard.");
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet]
        [Route("scoreboard/users")]
        public async Task<IActionResult> ScoreboardUser([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            _logger.LogInformation("Fetching user scoreboard: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
            try {
                if (pageNumber <= 0) 
                {
                    _logger.LogWarning("Invalid page number: {PageNumber}", pageNumber);
                    return BadRequest("Page number must be greater than 0.");
                }
                if (pageSize <= 0 || pageSize > 50) 
                {
                    _logger.LogWarning("Invalid page size: {PageSize}", pageSize); 
                    return BadRequest("Page size must be between 1 and 50.");
                }
                
                var pagedUserScoreboard = await _adminService.GetUserScoreboardAsync(pageNumber, pageSize);

                _logger.LogInformation("Successfully fetched user scoreboard");
                return Ok(pagedUserScoreboard);

            }catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user scoreboard.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}