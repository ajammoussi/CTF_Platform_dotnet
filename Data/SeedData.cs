using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CTF_Platform_dotnet.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(CTFContext context)
        {
            if (!await context.Users.AnyAsync())
            {
                var users = new List<User>
                {
                    new User { UserId = 1, Username = "user1", Email = "user1@example.com", PasswordHash = "$2y$10$ReiUaDHGrdYxTviU8sQfU.XfncFiebqwPaaZ5tklIMm0Hj.euOc2a", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-01 12:00:00"), Points = 100 },
                    new User { UserId = 2, Username = "user2", Email = "user2@example.com", PasswordHash = "$2y$10$s4iAIUjPCSvI6sTp2ReU1OW0elZ/MQNd73NZzjSXcEeup5sabgehy", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-02 12:00:00"), Points = 200 },
                    new User { UserId = 3, Username = "admin", Email = "admin@admin.com", PasswordHash = "$2y$10$NJ6G9sBamzWPIN8MQKnot.2kpzP3sk96LXGtLqM53w08rkdKBJTDS", Role = (RoleEnum)1, CreatedAt = DateTime.Parse("2023-10-01 12:00:00"), Points = 0 },
                    new User { UserId = 4, Username = "user4", Email = "user4@example.com", PasswordHash = "$2y$10$p.bPbsbnRSWKQeWwd6xZu.s4aIlknO50/7b4roNpmhnXN/wDvAeji", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-03 12:00:00"), Points = 150 },
                    new User { UserId = 5, Username = "user5", Email = "user5@example.com", PasswordHash = "$2y$10$NjK4cTuNNJCsKgokrPNTs..q9ukTUBkWpRvDTTM4EoAZGbUDFjYYy", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-04 12:00:00"), Points = 250 },
                    new User { UserId = 6, Username = "user6", Email = "user6@example.com", PasswordHash = "$2y$10$5Vl8AY.PVFqjCl1aO3orN.WhlztITQYAVGD4vc7BQStMVsZH17/GO", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-05 12:00:00"), Points = 300 },
                    new User { UserId = 7, Username = "user7", Email = "user7@example.com", PasswordHash = "$2y$10$uvBzfzXgr7pgsEAwDcxOcuMQ6.KXEWUfYSF4wjy5GYJpWeLk8vuu6", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-06 12:00:00"), Points = 50 },
                    new User { UserId = 8, Username = "user8", Email = "user8@example.com", PasswordHash = "$2y$10$cRmiRTbkqonRfIwMqECOe.B6WBll9u4B9WVAtjYxnZLFeP3R7OeRu", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-07 12:00:00"), Points = 400 },
                    new User { UserId = 9, Username = "user9", Email = "user9@example.com", PasswordHash = "$2y$10$cRmiRTbkqonRfIwMqECOe.B6WBll9u4B9WVAtjYxnZLFeP3R7OeRu", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-08 12:00:00"), Points = 100 },
                    new User { UserId = 10, Username = "user10", Email = "user10@example.com", PasswordHash = "$2y$10$gXy6z/GqMyIPUnsZEzX0k.pyuRskyi2f/xUttL59bxh9cHjgCTAVy", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-09 12:00:00"), Points = 200 },
                    new User { UserId = 11, Username = "user11", Email = "user11@example.com", PasswordHash = "$2y$10$vuOuiDF4DeO5cY8RpLxWqO6G09X3qg6JjfR.iw2zqQUjGR58Z3ZSO", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-10 12:00:00"), Points = 150 },
                    new User { UserId = 12, Username = "user12", Email = "user12@example.com", PasswordHash = "$2y$10$q2GzQnIq.3g5K5uMIAz1FOMbqy.EgH1nEnIvSJ0Drx00eXxSxc3Ly", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-11 12:00:00"), Points = 250 },
                    new User { UserId = 13, Username = "user13", Email = "user13@example.com", PasswordHash = "$2y$10$pja9QMH76e8npCTUUkUX3OMXXAy/mVFmYkDkQ02D1eNnNI8QMl4Gy", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-12 12:00:00"), Points = 300 },
                    new User { UserId = 14, Username = "user14", Email = "user14@example.com", PasswordHash = "$2y$10$AWSLoYG3CVNftnMMMaVR4ugW6jn277WIwLgGdteaIRcteRqw8Il1q", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-13 12:00:00"), Points = 50 },
                    new User { UserId = 15, Username = "user15", Email = "user15@example.com", PasswordHash = "$2y$10$IhGpsN7iuJ3IYOyG0wrvkO0nDm7BgfIQ50mbEA6I8lswWjW.wfXXW", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-14 12:00:00"), Points = 400 },
                    new User { UserId = 16, Username = "user16", Email = "user16@example.com", PasswordHash = "$2y$10$gEDKcdNvGlsie4yzco3gxe4VSXAXYMxPgeT5w/9I1VlQp9xyy80my", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-15 12:00:00"), Points = 100 },
                    new User { UserId = 17, Username = "user17", Email = "user17@example.com", PasswordHash = "$2y$10$XDv/e8BQY7tex9itJWADB.XBcgMdUvxdaUMuSnSWquJEoS19A7ctW", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-16 12:00:00"), Points = 200 },
                    new User { UserId = 18, Username = "user18", Email = "user18@example.com", PasswordHash = "$2y$10$KRtrYuex204WbdZiKrxL.u6rNVtpcTSr8qsZ0/iazYaIR/Fm//JRW", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-17 12:00:00"), Points = 150 },
                    new User { UserId = 19, Username = "user19", Email = "user19@example.com", PasswordHash = "$2y$10$lvgHEXLLiKgq4kHlzc02se6TVMXEmtHyp.ZEBs98R2xltjLR5XFB.", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-18 12:00:00"), Points = 250 },
                    new User { UserId = 20, Username = "user20", Email = "user20@example.com", PasswordHash = "$2y$10$IeshifzusdtjAJTXYKKNHugjutqk1cuAk3P/iyGU3SvK/HceSlef6", Role = (RoleEnum)0, CreatedAt = DateTime.Parse("2023-10-19 12:00:00"), Points = 300 }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }

            if (!await context.Teams.AnyAsync())
            {
                var teams = new List<Team>
            {
                new Team { TeamId = 1, TeamName = "Team Alpha", CreatedByUserId = 1, CreatedAt = DateTime.Parse("2023-10-01 12:00:00"), TotalPoints = 1500 },
                new Team { TeamId = 2, TeamName = "Team Beta", CreatedByUserId = 2, CreatedAt = DateTime.Parse("2023-10-02 12:00:00"), TotalPoints = 1200 },
                new Team { TeamId = 3, TeamName = "Team Gamma", CreatedByUserId = 3, CreatedAt = DateTime.Parse("2023-10-03 12:00:00"), TotalPoints = 900 },
                new Team { TeamId = 4, TeamName = "Team Delta", CreatedByUserId = 4, CreatedAt = DateTime.Parse("2023-10-04 12:00:00"), TotalPoints = 800 }
            };

                context.Teams.AddRange(teams);
                context.SaveChanges();
            }

            // Add users to teams
            // 1. Get the users to update (you might fetch them differently in your application)
            var usersToUpdateTeam1 = await context.Users.Where(u => new[] { 1, 4, 8, 12, 16, 20 }.Contains(u.UserId)).ToListAsync();
            var usersToUpdateTeam2 = await context.Users.Where(u => new[] { 2, 5, 9, 13, 17 }.Contains(u.UserId)).ToListAsync();
            var usersToUpdateTeam3 = await context.Users.Where(u => new[] { 3, 6, 10, 14, 18 }.Contains(u.UserId)).ToListAsync();
            var usersToUpdateTeam4 = await context.Users.Where(u => new[] { 7, 11, 15, 19 }.Contains(u.UserId)).ToListAsync();

            // 2. Update the TeamId for each group of users
            foreach (var user in usersToUpdateTeam1)
            {
                user.TeamId = 1;
            }
            foreach (var user in usersToUpdateTeam2)
            {
                user.TeamId = 2;
            }
            foreach (var user in usersToUpdateTeam3)
            {
                user.TeamId = 3;
            }
            foreach (var user in usersToUpdateTeam4)
            {
                user.TeamId = 4;
            }

            // 3. Save changes to the database
            await context.SaveChangesAsync();

            if (!await context.Challenges.AnyAsync())
            {
                var challenges = new List<Challenge>
            {
                new Challenge { ChallengeId = 1, Name = "Web Challenge 1", Description = "Find the hidden flag in the website.", Category = 0, Difficulty = 0, Points = 100, Flag = "FLAG{web1}", FilePath = null, CreatedAt = DateTime.Parse("2023-10-01 12:00:00"), IsActive = true },
                new Challenge { ChallengeId = 2, Name = "Forensics Challenge 1", Description = "Analyze the image to find the flag.", Category = (CategoryEnum)1, Difficulty = (DifficultyEnum)1, Points = 200, Flag = "FLAG{forensics1}", FilePath = "/path/to/file1", CreatedAt = DateTime.Parse("2023-10-02 12:00:00"), IsActive = true },
                new Challenge { ChallengeId = 3, Name = "Crypto Challenge 1", Description = "Decrypt the message to find the flag.", Category = (CategoryEnum)3, Difficulty = (DifficultyEnum)2, Points = 300, Flag = "FLAG{crypto1}", FilePath = null, CreatedAt = DateTime.Parse("2023-10-03 12:00:00"), IsActive = true },
                new Challenge { ChallengeId = 4, Name = "Reverse Engineering Challenge 1", Description = "Reverse the binary to find the flag.", Category = (CategoryEnum)2, Difficulty = (DifficultyEnum)2, Points = 400, Flag = "FLAG{reverse1}", FilePath = "/path/to/file2", CreatedAt = DateTime.Parse("2023-10-04 12:00:00"), IsActive = true },
                new Challenge { ChallengeId = 5, Name = "Binary Exploitation Challenge 1", Description = "Exploit the binary to get the flag.", Category = (CategoryEnum)4, Difficulty = (DifficultyEnum)1, Points = 250, Flag = "FLAG{binary1}", FilePath = null, CreatedAt = DateTime.Parse("2023-10-05 12:00:00"), IsActive = true },
                new Challenge { ChallengeId = 6, Name = "OSINT Challenge 1", Description = "Use open-source intelligence to find the flag.", Category = (CategoryEnum)5, Difficulty = (DifficultyEnum)0, Points = 150, Flag = "FLAG{osint1}", FilePath = null, CreatedAt = DateTime.Parse("2023-10-06 12:00:00"), IsActive = true },
                new Challenge { ChallengeId = 7, Name = "Miscellaneous Challenge 1", Description = "Solve the puzzle to find the flag.", Category = (CategoryEnum)6, Difficulty = (DifficultyEnum)0, Points = 100, Flag = "FLAG{misc1}", FilePath = null, CreatedAt = DateTime.Parse("2023-10-07 12:00:00"), IsActive = true },
                new Challenge { ChallengeId = 8, Name = "Web Challenge 2", Description = "Exploit the web application to find the flag.", Category = (CategoryEnum)0, Difficulty = (DifficultyEnum)1, Points = 200, Flag = "FLAG{web2}", FilePath = null, CreatedAt = DateTime.Parse("2023-10-08 12:00:00"), IsActive = true },
                new Challenge { ChallengeId = 9, Name = "Forensics Challenge 2", Description = "Analyze the network traffic to find the flag.", Category = (CategoryEnum)1, Difficulty = (DifficultyEnum)2, Points = 300, Flag = "FLAG{foren(Models.Enums.CategoryEnum)sics2}", FilePath = "/path/to/file3", CreatedAt = DateTime.Parse("2023-10-09 12:00:00"), IsActive = true },
                new Challenge { ChallengeId = 10, Name = "Crypto Challenge 2", Description = "Break the encryption to find the flag.", Category = (CategoryEnum)3, Difficulty = (DifficultyEnum)1, Points = 250, Flag = "FLAG{crypto2}", FilePath = null, CreatedAt = DateTime.Parse("2023-10-10 12:00:00"), IsActive = true }

            };

                context.Challenges.AddRange(challenges);
                context.SaveChanges();
            }

            if (!await context.Submissions.AnyAsync())
            {
                var submissions = new List<Submission>
            {
                new Submission { SubmissionId = 1, ChallengeId = 1, UserId = 1, TeamId = 1, SubmittedFlag = "FLAG{web1}", SubmittedAt = DateTime.Parse("2023-10-01 13:00:00"), IsCorrect = true },
                new Submission { SubmissionId = 2, ChallengeId = 2, UserId = 2, TeamId = 2, SubmittedFlag = "FLAG{forensics1}", SubmittedAt = DateTime.Parse("2023-10-02 14:00:00"), IsCorrect = true },
                new Submission { SubmissionId = 3, ChallengeId = 3, UserId = 2, TeamId = 2, SubmittedFlag = "FLAG{wrongflag}", SubmittedAt = DateTime.Parse("2023-10-03 15:00:00"), IsCorrect = false },
                new Submission { SubmissionId = 4, ChallengeId = 4, UserId = 4, TeamId = 1, SubmittedFlag = "FLAG{reverse1}", SubmittedAt = DateTime.Parse("2023-10-04 16:00:00"), IsCorrect = true },
                new Submission { SubmissionId = 5, ChallengeId = 5, UserId = 5, TeamId = 2, SubmittedFlag = "FLAG{binary1}", SubmittedAt = DateTime.Parse("2023-10-05 17:00:00"), IsCorrect = true },
                new Submission { SubmissionId = 6, ChallengeId = 6, UserId = 6, TeamId = 3, SubmittedFlag = "FLAG{osint1}", SubmittedAt = DateTime.Parse("2023-10-06 18:00:00"), IsCorrect = true },
                new Submission { SubmissionId = 7, ChallengeId = 7, UserId = 7, TeamId = 4, SubmittedFlag = "FLAG{misc1}", SubmittedAt = DateTime.Parse("2023-10-07 19:00:00"), IsCorrect = true },
                new Submission { SubmissionId = 8, ChallengeId = 8, UserId = 8, TeamId = 1, SubmittedFlag = "FLAG{web2}", SubmittedAt = DateTime.Parse("2023-10-08 20:00:00"), IsCorrect = true },
                new Submission { SubmissionId = 9, ChallengeId = 9, UserId = 9, TeamId = 2, SubmittedFlag = "FLAG{forensics2}", SubmittedAt = DateTime.Parse("2023-10-09 21:00:00"), IsCorrect = true },
                new Submission { SubmissionId = 10, ChallengeId = 10, UserId = 10, TeamId = 3, SubmittedFlag = "FLAG{crypto2}", SubmittedAt = DateTime.Parse("2023-10-10 22:00:00"), IsCorrect = true }
            };

                context.Submissions.AddRange(submissions);
                context.SaveChanges();
            }
        }
    }
}
