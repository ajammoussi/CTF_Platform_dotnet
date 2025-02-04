using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CTF_Platform_dotnet.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string TeamName { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int TotalPoints { get; set; } = 0;

        // For password reset
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }

        // Navigation properties
        public User? CreatedByUser { get; set; }
        public ICollection<User> Users {get;set;} = new List<User>(); 
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>(); 
    }
}