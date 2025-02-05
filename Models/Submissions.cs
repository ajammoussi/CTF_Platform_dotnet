using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTF_Platform_dotnet.Models
{
    public class Submission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int SubmissionId { get; set; }

        [Required]
        public required int ChallengeId { get; set; }

        public required int UserId { get; set; } 

        public required int TeamId { get; set; }

        [Required]
        [MaxLength(255)]
        public required string SubmittedFlag { get; set; } = string.Empty;

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public required bool IsCorrect { get; set; }

        // Navigation properties
        public Challenge Challenge { get; set; }
        public User? User { get; set; }
        public Team? Team { get; set; }
    }
}
