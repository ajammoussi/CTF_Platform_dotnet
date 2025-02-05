namespace CTF_Platform_dotnet.DTOs
{
    public class SubmissionAdminDto
    {
        public int SubmissionId {get; set; }
        public int ChallengeId {get; set; }
        public int? UserId { get; set; }
        public int? TeamId { get; set; }
        public string? SubmittedFlag { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool? IsCorrect { get; set; }
    }
}
