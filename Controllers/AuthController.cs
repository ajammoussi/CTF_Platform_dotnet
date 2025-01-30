using CTF_Platform_dotnet.Auth;
using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.Models.Dtos;
using CTF_Platform_dotnet.Models.DTOs;
using CTF_Platform_dotnet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CTF_Platform_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly CTFContext _cTFContext;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;

        public AuthController(CTFContext cTFContext, JwtSettings jwtSettings, IConfiguration configuration, IUserService userService, ITeamService teamService)
        {
            _cTFContext = cTFContext;
            _jwtSettings = jwtSettings;
            _configuration = configuration;
            _userService = userService;
            _teamService = teamService;
        }

        [HttpPost("token")]
        public IActionResult GenerateToken()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "testuser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // hash the password of the dto with bcrypt
           
            var userByUsername = (await _userService.GetUsersByPredicateAsync(u => u.Username == dto.Username)).FirstOrDefault();
            if (userByUsername != null)
            {
                Console.WriteLine("User: " + userByUsername.ToString());
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, userByUsername.PasswordHash);
            Console.WriteLine("is password valid: " + isPasswordValid);
            var user = (await _userService.GetUsersByPredicateAsync(u => u.Username == dto.Username && isPasswordValid)).FirstOrDefault();
            if (user != null)
            {
                Console.WriteLine("User: " + user.ToString());

                // Ensure that the user properties are not null
                var userId = user.UserId.ToString() ?? throw new ArgumentNullException(nameof(user.UserId));
                var email = user.Email ?? throw new ArgumentNullException(nameof(user.Email));

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Role", user.Role.ToString()),            
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("Email", user.Email),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["JwtSettings:Issuer"],
                    _configuration["JwtSettings:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signIn
                );
                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                var userResponse = new
                {
                    user.UserId,
                    user.Username,
                    user.Email,
                    role = user.Role.ToString(), // Convert the enum to string
                    user.CreatedAt,
                    user.Points,
                    user.TeamId,
                    user.Submissions,
                    user.SupportTickets
                };

                return Ok(new {Token = tokenValue, User = userResponse });
            }

            return NoContent();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignupDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid user data.");
            }

            using (var transaction = await _cTFContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Hash the password of the dto with bcrypt
                    var dtoPasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                    var user = new User
                    {
                        Username = dto.Username,
                        Email = dto.Email,
                        PasswordHash = dtoPasswordHash,
                        CreatedAt = DateTime.UtcNow,
                        Points = 0
                    };

                    await _userService.AddUserAsync(user);

                    var registeredUser = (await _userService.GetUsersByPredicateAsync(u => u.Username == dto.Username)).FirstOrDefault();

                    if (registeredUser == null)
                    {
                        throw new Exception("User registration failed.");
                    }

                    Console.WriteLine("User registered successfully");

                    // Create a team with the username as the team name
                    var team = new Team
                    {
                        TeamName = registeredUser.Username,
                        CreatedByUserId = registeredUser.UserId,
                        CreatedAt = DateTime.UtcNow,
                        TotalPoints = 0
                    };

                    await _teamService.AddTeamAsync(team);

                    var registeredTeam = (await _teamService.GetTeamsByPredicateAsync(t => t.TeamName == dto.Username)).FirstOrDefault();

                    if (registeredTeam == null)
                    {
                        throw new Exception("Team creation failed.");
                    }

                    Console.WriteLine("Team created successfully"); 

                    // Assign the created team ID to the user
                    registeredUser.TeamId = registeredTeam.TeamId;
                    registeredUser.Team = registeredTeam;

                    await _userService.UpdateUserAsync(registeredUser);

                    Console.WriteLine("User assigned to team successfully");

                    // Commit the transaction
                    await transaction.CommitAsync();

                    return Ok(new { Message = "User and its team registered successfully", User = user, Team = team });
                }
                catch (Exception e)
                {
                    // Rollback the transaction if any error occurs
                    await transaction.RollbackAsync();
                    return StatusCode(500, "An error occurred while registering the user. " + e.Message + e.InnerException?.Message);
                }
            }
        }

    } 
}