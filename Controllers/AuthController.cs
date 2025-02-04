using CTF_Platform_dotnet.Auth;
using CTF_Platform_dotnet.DTOs;
using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.Services;
using CTF_Platform_dotnet.Services.EmailSender;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Helpers.Mail;
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
        private readonly IEmailSender _emailSender;

        public AuthController(CTFContext cTFContext, JwtSettings jwtSettings, IConfiguration configuration, IUserService userService, ITeamService teamService, IEmailSender emailSender)
        {
            _cTFContext = cTFContext;
            _jwtSettings = jwtSettings;
            _configuration = configuration;
            _userService = userService;
            _teamService = teamService;
            _emailSender = emailSender;
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
            if (userByUsername == null)
            {
                return BadRequest("Invalid username.");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, userByUsername.PasswordHash);
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
                var expiry = DateTime.UtcNow.AddMinutes(60);
                var token = new JwtSecurityToken(
                    _configuration["JwtSettings:Issuer"],
                    _configuration["JwtSettings:Audience"],
                    claims,
                    expires: expiry,
                    signingCredentials: signIn
                );
                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                user.LoginToken = tokenValue;
                user.LoginTokenExpiry = expiry;
                await _userService.UpdateUserAsync(user);

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
                    user.SupportTickets,
                };

                return Ok(new {Token = tokenValue, User = userResponse });
            }

            return BadRequest("Invalid password.");
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignupDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid user data.");
            }

            // Check if the username or email already exists
            var existingUser = await _userService.GetUsersByPredicateAsync(u => u.Username == dto.Username || u.Email == dto.Email);
            if (existingUser.Any())
            {
                return BadRequest("Username or email already exists.");
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
                        Role = Models.Enums.RoleEnum.Participant,
                        CreatedAt = DateTime.UtcNow,
                        Points = 0
                    };

                    await _userService.AddUserAsync(user);

                    var registeredUser = (await _userService.GetUsersByPredicateAsync(u => u.Username == dto.Username)).FirstOrDefault();

                    if (registeredUser == null)
                    {
                        throw new Exception("User registration failed.");
                    }

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

                    // Assign the created team ID to the user
                    registeredUser.TeamId = registeredTeam.TeamId;
                    registeredUser.Team = registeredTeam;

                    await _userService.UpdateUserAsync(registeredUser);

                    // Commit the transaction
                    await transaction.CommitAsync();

                    // Automatically log in the user
                    var loginDto = new LoginDto
                    {
                        Username = dto.Username,
                        Password = dto.Password
                    };

                    return await Login(loginDto);
                }
                catch (Exception e)
                {
                    // Rollback the transaction if any error occurs
                    await transaction.RollbackAsync();
                    return StatusCode(500, "An error occurred while registering the user. " + e.Message + e.InnerException?.Message);
                }
            }
        }

        // logout 

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutDto dto)
        {
            var user = (await _userService.GetUsersByPredicateAsync(u => u.LoginToken == dto.Token && u.UserId == dto.UserId)).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Invalid token.");
            }
            user.LoginToken = null;
            user.LoginTokenExpiry = null;
            await _userService.UpdateUserAsync(user);
            return Ok("Logged out successfully.");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var user = (await _userService.GetUsersByPredicateAsync(u => u.Email == dto.Email)).FirstOrDefault();
            if (user == null)
            {
                // For security reasons, don't reveal that the user doesn't exist
                return Ok("If your email is registered, you will receive a password reset link.");
            }

            // Generate a password reset token
            var resetToken = Guid.NewGuid().ToString();
            user.ResetToken = resetToken;
            user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1); // Token expires in 1 hour

            await _userService.UpdateUserAsync(user);

            // Send the reset token via email
            var resetLink = $"{_configuration["Frontend:BaseUrl"]}/reset-password?token={resetToken}";
            var emailSubject = "Password Reset Request";
            var emailBody = $"Please reset your password by clicking <a href='{resetLink}'>here</a>.";

            Console.WriteLine("Reset link: " + resetLink);
            Console.WriteLine("Email body: " + emailBody);
            Console.WriteLine("Email subject: " + emailSubject);

            try
            {
                await _emailSender.SendEmailAsync(user.Email, emailSubject, emailBody);
                return Ok("Password reset link has been sent to your email.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to send email. Please try again later.");
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var user = (await _userService.GetUsersByPredicateAsync(u => u.ResetToken == dto.Token && u.ResetTokenExpiry > DateTime.UtcNow)).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Invalid or expired token.");
            }

            // Hash the new password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            await _userService.UpdateUserAsync(user);

            return Ok("Password has been reset successfully.");
        }

    } 
}