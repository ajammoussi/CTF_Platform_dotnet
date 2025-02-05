using CTF_Platform_dotnet.Models.Enums;

namespace CTF_Platform_dotnet.DTOs
{
    public class ChallengeDto
    {
        public int ChallengeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Difficulty { get; set; }
        public int Points { get; set; }
        public string FilePath { get; set; }
    }
}
