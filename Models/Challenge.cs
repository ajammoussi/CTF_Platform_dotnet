using CTF_Platform_dotnet.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CTF_Platform_dotnet.Models
{
    public class Challenge
    {
        [Key]
        public int ChallengeId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public CategoryEnum Category { get; set; }

        [MaxLength(20)]
        public DifficultyEnum Difficulty { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Flag { get; set; }

        [MaxLength(255)]
        public string? FilePath { get; set; }

        // Navigation properties
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
