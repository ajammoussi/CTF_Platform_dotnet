using System.ComponentModel.DataAnnotations;

namespace CTF_Platform_dotnet.Models.DTOs
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
