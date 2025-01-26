using System.ComponentModel.DataAnnotations;

namespace CTF_Platform_dotnet.Models
{
    public class SupportTicket
    {
        [Key]
        public int TicketId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsResolved { get; set; } = false;

        // Navigation properties
        public User User { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; } // Link to chat messages
    }
}
