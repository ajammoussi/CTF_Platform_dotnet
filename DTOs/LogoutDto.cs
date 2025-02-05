namespace CTF_Platform_dotnet.DTOs
{
    public class LogoutDto
    {
        public required string Token { get; set; }
        public required int UserId { get; set; }
    }
}
