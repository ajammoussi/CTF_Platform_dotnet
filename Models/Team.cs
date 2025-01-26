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
        public string TeamName { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int TotalPoints { get; set; } = 0;

        // Navigation properties
        public User CreatedByUser { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>(); // Initialize here
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>(); // Optional: Initialize if needed
    }
}