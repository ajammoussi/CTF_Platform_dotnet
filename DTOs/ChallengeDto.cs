using CTF_Platform_dotnet.Models.Enums;

namespace CTF_Platform_dotnet.DTOs
{
    public class ChallengeDto
    {
        public int ChallengeId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string Category { get; set; }
        public string Difficulty { get; set; }
        public int Points { get; set; }
        public string FilePath { get; set; }
    }
}
