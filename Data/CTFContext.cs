using CTF_Platform_dotnet.Models;
using Microsoft.EntityFrameworkCore;

public class CTFContext : DbContext
{
    public CTFContext(DbContextOptions<CTFContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<SupportTicket> SupportTickets { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<TeamMember>()
            .HasOne(tm => tm.Team)
            .WithMany(t => t.TeamMembers)
            .HasForeignKey(tm => tm.TeamId);

        modelBuilder.Entity<TeamMember>()
            .HasOne(tm => tm.User)
            .WithMany(u => u.TeamMembers)
            .HasForeignKey(tm => tm.UserId);

        modelBuilder.Entity<Submission>()
            .HasOne(s => s.Challenge)
            .WithMany(c => c.Submissions)
            .HasForeignKey(s => s.ChallengeId);

        modelBuilder.Entity<Submission>()
            .HasOne(s => s.User)
            .WithMany(u => u.Submissions)
            .HasForeignKey(s => s.UserId);

        modelBuilder.Entity<Submission>()
            .HasOne(s => s.Team)
            .WithMany(t => t.Submissions)
            .HasForeignKey(s => s.TeamId);

        modelBuilder.Entity<SupportTicket>()
            .HasOne(st => st.User)
            .WithMany(u => u.SupportTickets)
            .HasForeignKey(st => st.UserId);

        // Configure ChatMessage relationships
        modelBuilder.Entity<ChatMessage>()
            .HasOne(cm => cm.SupportTicket)
            .WithMany(st => st.ChatMessages)
            .HasForeignKey(cm => cm.TicketId);

        modelBuilder.Entity<ChatMessage>()
            .HasOne(cm => cm.SenderUser)
            .WithMany()
            .HasForeignKey(cm => cm.SenderUserId);
    }
}