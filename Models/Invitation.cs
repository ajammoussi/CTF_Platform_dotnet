using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTF_Platform_dotnet.Models
{
    public class Invitation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime Expiration { get; set; }

        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }

        [Required]
        public Team Team { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
    }
}