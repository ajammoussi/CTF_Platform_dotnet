using System.ComponentModel.DataAnnotations;

namespace CTF_Platform_dotnet.Models
{
    public class Submission
    {
        [Key]
        public int SubmissionId { get; set; }

        [Required]
        public int ChallengeId { get; set; }

        public int UserId { get; set; } 

        public int TeamId { get; set; }

        [Required]
        [MaxLength(255)]
        public required string SubmittedFlag { get; set; } = string.Empty;

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public bool? IsCorrect { get; set; }

        // Navigation properties
        public Challenge Challenge { get; set; }
        public User? User { get; set; }
        public Team? Team { get; set; }
    }
}
