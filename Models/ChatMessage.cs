using System.ComponentModel.DataAnnotations;

namespace CTF_Platform_dotnet.Models
{
    public class ChatMessage
    {
        [Key]
        public int ChatMessageId { get; set; }

        [Required]
        public int TicketId { get; set; } // Link to the support ticket

        [Required]
        public int SenderUserId { get; set; } // User who sent the message (can be a support agent or the user)

        [Required]
        public string Message { get; set; } // The chat message content

        public DateTime SentAt { get; set; } = DateTime.UtcNow; // Timestamp of the message

        // Navigation properties
        public SupportTicket SupportTicket { get; set; }
        public User SenderUser { get; set; }
    }
}
