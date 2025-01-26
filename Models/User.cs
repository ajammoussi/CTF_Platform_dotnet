using CTF_Platform_dotnet.Models.Enums;
using System.ComponentModel.DataAnnotations;


namespace CTF_Platform_dotnet.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        public RoleEnum Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int Points { get; set; } = 0;


        // Navigation properties
        public ICollection<TeamMember> TeamMembers { get; set; }
        public ICollection<Submission> Submissions { get; set; }
        public ICollection<Challenge> CreatedChallenges { get; set; }
        public ICollection<SupportTicket> SupportTickets { get; set; }
    }
}
