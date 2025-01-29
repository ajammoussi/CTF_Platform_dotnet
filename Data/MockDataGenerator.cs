using Bogus;
using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.Models.Enums;

public static class MockDataGenerator
{
    // Generate mock users
    public static List<User> GenerateUsers(int count)
    {
        var userFaker = new Faker<User>()
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.PasswordHash, f => f.Random.String(64))
            .RuleFor(u => u.Role, f => f.PickRandom<RoleEnum>())
            .RuleFor(u => u.CreatedAt, f => f.Date.Past())
            .RuleFor(u => u.Points, f => f.Random.Int(0, 1000));

        return userFaker.Generate(count);
    }

    // Generate mock teams and link them to users via TeamMember
    public static List<Team> GenerateTeams(int count, List<User> users)
    {
        var teamFaker = new Faker<Team>()
            .RuleFor(t => t.TeamName, f => f.Hacker.Noun())
            .RuleFor(t => t.CreatedByUserId, f => f.PickRandom(users).UserId)
            .RuleFor(t => t.CreatedAt, f => f.Date.Past())
            .RuleFor(t => t.TotalPoints, f => f.Random.Int(0, 1000));
        return teamFaker.Generate(count);
    }

    // Generate mock challenges and link them to users (as creators)
    public static List<Challenge> GenerateChallenges(int count, List<User> users)
    {
        var challengeFaker = new Faker<Challenge>()
            .RuleFor(c => c.Name, f => f.Hacker.Phrase())
            .RuleFor(c => c.Description, f => f.Lorem.Paragraph())
            .RuleFor(c => c.Category, f => f.PickRandom<CategoryEnum>())
            .RuleFor(c => c.Difficulty, f => f.PickRandom<DifficultyEnum>())
            .RuleFor(c => c.Points, f => f.Random.Int(100, 1000))
            .RuleFor(c => c.Flag, f => f.Random.String(32))
            .RuleFor(c => c.FilePath, f => f.System.FilePath());

        return challengeFaker.Generate(count);
    }

    // Generate mock submissions and link them to users or teams
    public static List<Submission> GenerateSubmissions(int count, List<User> users, List<Team> teams, List<Challenge> challenges)
    {
        var submissionFaker = new Faker<Submission>()
            .RuleFor(s => s.SubmittedFlag, f => f.Random.String(32))
            .RuleFor(s => s.SubmittedAt, f => f.Date.Past())
            .RuleFor(s => s.IsCorrect, f => f.Random.Bool());

        var submissions = submissionFaker.Generate(count);

        // Assign submissions to users or teams
        var random = new Random();
        foreach (var submission in submissions)
        {
            submission.ChallengeId = challenges[random.Next(challenges.Count)].ChallengeId;

            if (random.Next(2) == 0) // 50% chance to assign to a user or team
            {
                submission.UserId = users[random.Next(users.Count)].UserId;
            }
            else
            {
                submission.TeamId = teams[random.Next(teams.Count)].TeamId;
            }
        }

        return submissions;
    }

    // Generate mock support tickets and link them to users
    public static List<SupportTicket> GenerateSupportTickets(int count, List<User> users)
    {
        var ticketFaker = new Faker<SupportTicket>()
            .RuleFor(st => st.Subject, f => f.Lorem.Sentence())
            .RuleFor(st => st.Description, f => f.Lorem.Paragraph())
            .RuleFor(st => st.CreatedAt, f => f.Date.Past())
            .RuleFor(st => st.IsResolved, f => f.Random.Bool());

        var tickets = ticketFaker.Generate(count);

        // Assign tickets to users
        var random = new Random();
        foreach (var ticket in tickets)
        {
            ticket.UserId = users[random.Next(users.Count)].UserId;
        }

        return tickets;
    }
    
}

