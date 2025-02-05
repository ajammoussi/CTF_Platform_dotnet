
namespace CTF_Platform_dotnet.DTOs
{
    public class TeamDto
    {
        public int TeamId { get; set; }
        public required string TeamName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int TotalPoints { get; set; } = 0;
        public int TotalSolves { get; set; } = 0;
    }
}