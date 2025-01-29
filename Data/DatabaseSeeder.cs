namespace CTF_Platform_dotnet.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed(CTFContext context)
        {
            if (!context.Users.Any())
            {
                // Generate mock data
                var users = MockDataGenerator.GenerateUsers(50); // 50 users
                var teams = MockDataGenerator.GenerateTeams(10, users); // 10 teams
                var challenges = MockDataGenerator.GenerateChallenges(20, users); // 20 challenges
                var submissions = MockDataGenerator.GenerateSubmissions(100, users, teams, challenges); // 100 submissions
                var supportTickets = MockDataGenerator.GenerateSupportTickets(30, users); // 30 support tickets

                // Add data to the database
                context.Users.AddRange(users);
                context.Teams.AddRange(teams);
                context.Challenges.AddRange(challenges);
                context.Submissions.AddRange(submissions);
                context.SupportTickets.AddRange(supportTickets);

                // Save changes
                context.SaveChanges();
            }
        }
    }
}
