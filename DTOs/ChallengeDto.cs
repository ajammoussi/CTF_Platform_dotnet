using CTF_Platform_dotnet.Models.Enums;

namespace CTF_Platform_dotnet.DTOs
{
    public class ChallengeDto
    {
        public int ChallengeId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public CategoryEnum Category { get; set; }
        public DifficultyEnum Difficulty { get; set; }
        public int Points { get; set; }
        public string FilePath { get; set; }
    }
}
