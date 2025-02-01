namespace CTF_Platform_dotnet.DTOs
{
    public class TicketDto
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsResolved { get; set; }
    }
}
