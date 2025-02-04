using CTF_Platform_dotnet.Models;
using Microsoft.EntityFrameworkCore;

public class CTFContext : DbContext
{
    public CTFContext(DbContextOptions<CTFContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<SupportTicket> SupportTickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<User>()
            .Property(u => u.UserId)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasOne(u=>u.Team)
            .WithMany(t=>t.Users)
            .HasForeignKey(u=>u.TeamId);

        modelBuilder.Entity<Team>()
            .HasOne(t=>t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t=>t.CreatedByUserId);

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
    }
}