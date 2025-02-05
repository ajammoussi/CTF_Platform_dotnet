using System.ComponentModel.DataAnnotations;

namespace CTF_Platform_dotnet.DTOs
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
