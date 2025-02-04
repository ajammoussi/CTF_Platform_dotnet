using CTF_Platform_dotnet.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTF_Platform_dotnet.Models
{
    public class Challenge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChallengeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)] // Limit description length
        public string Description { get; set; }

        [Required]
        [EnumDataType(typeof(CategoryEnum))] // Add enum validation
        public CategoryEnum Category { get; set; }

        [Required] // If you want difficulty to be mandatory
        [EnumDataType(typeof(DifficultyEnum))]
        public DifficultyEnum Difficulty { get; set; }

        [Required]
        [Range(1, 10000)] // Prevent negative/zero points
        public int Points { get; set; }

        [Required]
        [MaxLength(255)]
        public string Flag { get; set; }

        [MaxLength(255)]
        public string? FilePath { get; set; } // Make nullable

        // Add basic audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Navigation property
        public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
