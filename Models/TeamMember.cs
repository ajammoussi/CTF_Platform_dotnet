using System.ComponentModel.DataAnnotations;

namespace CTF_Platform_dotnet.Models
{
    public class TeamMember
    {
        [Key]
        public int TeamMemberId { get; set; }

        [Required]
        public int TeamId { get; set; }

        [Required]
        public int UserId { get; set; }

        // Navigation properties
        public Team Team { get; set; }
        public User User { get; set; }
    }
}
