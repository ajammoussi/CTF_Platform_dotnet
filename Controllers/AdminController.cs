using Microsoft.AspNetCore.Mvc;
using CTF_Platform_dotnet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using CTF_Platform_dotnet.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using CTF_Platform_dotnet.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace CTF_Platform_dotnet.Controllers{
    [Authorize(Policy = "AdminOnly")]
    [Route("api/admin")]
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
        private readonly IMapper _mapper;



        public AdminController(
            IRepository<User> userRepository, 
            IRepository<Challenge> challengeRepository, 
            IRepository<Submission> submissionRepository, 
            IRepository<Team> teamRepository, 
            IRepository<SupportTicket> supportTicketRepository,
            IAdminService adminService,
            ILogger<AdminController> logger,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _challengeRepository = challengeRepository;
            _submissionRepository = submissionRepository;
            _teamRepository = teamRepository;
            _supportTicketRepository = supportTicketRepository;
            _adminService = adminService;
            _logger = logger;
            _mapper = mapper;
        }


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

        [ValidatePagination(30)] // custom action filter that validates pagination parameters
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> Users([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Fetching users: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
            try {
                
                var pagedUsers  = await _userRepository.GetPagedAsync(pageNumber, pageSize);

                //// Map the user entities to UserDto objects using AutoMapper(avoiding sensitive or unnecessary data)
                var users = _mapper.Map<List<UserDto>>(pagedUsers.Items);

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

        [ValidatePagination(30)]
        [HttpGet]
        [Route("challenges")]
        public async Task<IActionResult> Challenges([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Fetching challenges: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
            try {
                
                var pagedChallenges  = await _challengeRepository.GetPagedAsync(pageNumber, pageSize);

                var challenges = _mapper.Map<List<ChallengeDto>>(pagedChallenges.Items);

                var pagedResult = new PagedResponse<ChallengeDto>
                (
                    challenges.ToList(),
                    pageNumber,
                    pageSize,
                    pagedChallenges.TotalItems
                );

                _logger.LogInformation("Successfully fetched challenges");
                return Ok(pagedResult);
            } catch (Exception ex) {
                _logger.LogError(ex, "Error fetching challenges.");
                return StatusCode(500, ex.Message);
            }
        }

        [ValidatePagination(30)]
        [HttpGet]
        [Route("submissions")]
        public async Task<IActionResult> Submissions ([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Fetching submissions: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
            try {

                var pagedSubmissions  = await _submissionRepository.GetPagedAsync(pageNumber, pageSize);

                var submissions = _mapper.Map<List<SubmissionDto>>(pagedSubmissions.Items);

                var pagedResult = new PagedResponse<SubmissionDto>
                (
                    submissions.ToList(),
                    pageNumber,
                    pageSize,
                    pagedSubmissions.TotalItems
                );

                _logger.LogInformation("Successfully fetched submissions");
                return Ok(pagedResult);
            } catch (Exception ex) {
                _logger.LogError(ex, "Error fetching submissions.");
                return StatusCode(500, ex.Message);
            }
        }

        [ValidatePagination(30)] 
        [HttpGet]
        [Route("teams")]
        public async Task<IActionResult> Teams([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Fetching teams: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
            try {

                var pagedTeams  = await _teamRepository.GetPagedAsync(pageNumber, pageSize);

                var teams = _mapper.Map<List<TeamDto>>(pagedTeams.Items);
                
                var pagedResult = new PagedResponse<TeamDto>
                (
                    teams.ToList(),
                    pageNumber,
                    pageSize,
                    pagedTeams.TotalItems
                );
                
                _logger.LogInformation("Successfully fetched challenges");
                return Ok(pagedResult);
            } catch (Exception ex) {
                _logger.LogError(ex, "Error fetching challenges.");
                return StatusCode(500, ex.Message);
            }
        }

        [ValidatePagination(30)]
        [HttpGet]
        [Route("tickets")]
        public async Task<IActionResult> SupportTickets([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Fetching support tickets: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
            try {

                var pagedSupportTickets  = await _supportTicketRepository.GetPagedAsync(pageNumber, pageSize);

                var tickets = _mapper.Map<List<TicketDto>>(pagedSupportTickets.Items);

                var pagedResult = new PagedResponse<TicketDto>
                (
                    tickets.ToList(),
                    pageNumber,
                    pageSize,
                    pagedSupportTickets.TotalItems
                );

                _logger.LogInformation("Successfully fetched support tickets");
                return Ok(pagedResult);
            } catch (Exception ex) {
                _logger.LogError(ex, "Error fetching support tickets.");
                return StatusCode(500, ex.Message);
            }
        }

        [ValidatePagination(30)]
        [HttpGet]
        [Route("scoreboard/teams")]
        public async Task<IActionResult> ScoreboardTeam ([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Fetching team scoreboard: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
            try {

                var pagedTeamScoreboard = await _adminService.GetTeamScoreboardAsync(pageNumber, pageSize);
                
                _logger.LogInformation("Successfully fetched team scoreboard");
                return Ok(pagedTeamScoreboard);

            } catch (Exception ex) {
                _logger.LogError(ex, "Error fetching team scoreboard.");
                return StatusCode(500, ex.Message);
            }
        }
        
        [ValidatePagination(30)]
        [HttpGet]
        [Route("scoreboard/users")]
        public async Task<IActionResult> ScoreboardUser([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Fetching user scoreboard: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
            try {
                
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