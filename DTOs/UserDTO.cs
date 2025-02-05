using CTF_Platform_dotnet.Models.Enums;

namespace CTF_Platform_dotnet.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Points { get; set; }
        public int TeamId { get; set; }
        public int TotalSolves { get; set; } = 0;
    }
}