using System.ComponentModel.DataAnnotations;

namespace CTF_Platform_dotnet.Models
{
    public class Submission
    {
        [Key]
        public int SubmissionId { get; set; }

        [Required]
        public int ChallengeId { get; set; }

        public int? UserId { get; set; } // Nullable for team submissions

        public int? TeamId { get; set; } // Nullable for individual submissions

        [Required]
        [MaxLength(255)]
        public required string SubmittedFlag { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public bool? IsCorrect { get; set; }

        // Navigation properties
        public required Challenge Challenge { get; set; }
        public User? User { get; set; }
        public Team? Team { get; set; }
    }
}
