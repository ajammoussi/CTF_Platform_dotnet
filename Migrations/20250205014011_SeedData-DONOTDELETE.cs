using CTF_Platform_dotnet.Models.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTF_Platform_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataDONOTDELETE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Users
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 1, "user1", "user1@example.com", "$2y$10$ReiUaDHGrdYxTviU8sQfU.XfncFiebqwPaaZ5tklIMm0Hj.euOc2a", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-01 12:00:00"), DateTimeKind.Utc), 100 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 2, "user2", "user2@example.com", "$2y$10$s4iAIUjPCSvI6sTp2ReU1OW0elZ/MQNd73NZzjSXcEeup5sabgehy", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-02 12:00:00"), DateTimeKind.Utc), 200 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 3, "admin", "admin@admin.com", "$2y$10$NJ6G9sBamzWPIN8MQKnot.2kpzP3sk96LXGtLqM53w08rkdKBJTDS", 1, DateTime.SpecifyKind(DateTime.Parse("2023-10-01 12:00:00"), DateTimeKind.Utc), 0 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 4, "user4", "user4@example.com", "$2y$10$p.bPbsbnRSWKQeWwd6xZu.s4aIlknO50/7b4roNpmhnXN/wDvAeji", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-03 12:00:00"), DateTimeKind.Utc), 150 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 5, "user5", "user5@example.com", "$2y$10$NjK4cTuNNJCsKgokrPNTs..q9ukTUBkWpRvDTTM4EoAZGbUDFjYYy", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-04 12:00:00"), DateTimeKind.Utc), 250 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 6, "user6", "user6@example.com", "$2y$10$5Vl8AY.PVFqjCl1aO3orN.WhlztITQYAVGD4vc7BQStMVsZH17/GO", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-05 12:00:00"), DateTimeKind.Utc), 300 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 7, "user7", "user7@example.com", "$2y$10$uvBzfzXgr7pgsEAwDcxOcuMQ6.KXEWUfYSF4wjy5GYJpWeLk8vuu6", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-06 12:00:00"), DateTimeKind.Utc), 50 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 8, "user8", "user8@example.com", "$2y$10$cRmiRTbkqonRfIwMqECOe.B6WBll9u4B9WVAtjYxnZLFeP3R7OeRu", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-07 12:00:00"), DateTimeKind.Utc), 400 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 9, "user9", "user9@example.com", "$2y$10$cRmiRTbkqonRfIwMqECOe.B6WBll9u4B9WVAtjYxnZLFeP3R7OeRu", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-08 12:00:00"), DateTimeKind.Utc), 100 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 10, "user10", "user10@example.com", "$2y$10$gXy6z/GqMyIPUnsZEzX0k.pyuRskyi2f/xUttL59bxh9cHjgCTAVy", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-09 12:00:00"), DateTimeKind.Utc), 200 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 11, "user11", "user11@example.com", "$2y$10$vuOuiDF4DeO5cY8RpLxWqO6G09X3qg6JjfR.iw2zqQUjGR58Z3ZSO", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-10 12:00:00"), DateTimeKind.Utc), 150 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 12, "user12", "user12@example.com", "$2y$10$q2GzQnIq.3g5K5uMIAz1FOMbqy.EgH1nEnIvSJ0Drx00eXxSxc3Ly", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-11 12:00:00"), DateTimeKind.Utc), 250 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 13, "user13", "user13@example.com", "$2y$10$pja9QMH76e8npCTUUkUX3OMXXAy/mVFmYkDkQ02D1eNnNI8QMl4Gy", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-12 12:00:00"), DateTimeKind.Utc), 300 });
            migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
            values: new object[] { 14, "user14", "user14@example.com", "$2y$10$AWSLoYG3CVNftnMMMaVR4ugW6jn277WIwLgGdteaIRcteRqw8Il1q", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-13 12:00:00"), DateTimeKind.Utc), 50 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 15, "user15", "user15@example.com", "$2y$10$IhGpsN7iuJ3IYOyG0wrvkO0nDm7BgfIQ50mbEA6I8lswWjW.wfXXW", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-14 12:00:00"), DateTimeKind.Utc), 400 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 16, "user16", "user16@example.com", "$2y$10$gEDKcdNvGlsie4yzco3gxe4VSXAXYMxPgeT5w/9I1VlQp9xyy80my", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-15 12:00:00"), DateTimeKind.Utc), 100 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 17, "user17", "user17@example.com", "$2y$10$XDv/e8BQY7tex9itJWADB.XBcgMdUvxdaUMuSnSWquJEoS19A7ctW", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-16 12:00:00"), DateTimeKind.Utc), 200 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 18, "user18", "user18@example.com", "$2y$10$KRtrYuex204WbdZiKrxL.u6rNVtpcTSr8qsZ0/iazYaIR/Fm//JRW", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-17 12:00:00"), DateTimeKind.Utc), 150 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 19, "user19", "user19@example.com", "$2y$10$lvgHEXLLiKgq4kHlzc02se6TVMXEmtHyp.ZEBs98R2xltjLR5XFB.", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-18 12:00:00"), DateTimeKind.Utc), 250 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "Email", "PasswordHash", "Role", "CreatedAt", "Points" },
                values: new object[] { 20, "user20", "user20@example.com", "$2y$10$IeshifzusdtjAJTXYKKNHugjutqk1cuAk3P/iyGU3SvK/HceSlef6", 0, DateTime.SpecifyKind(DateTime.Parse("2023-10-19 12:00:00"), DateTimeKind.Utc), 300 });

            // Teams
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "TeamName", "CreatedByUserId", "CreatedAt", "TotalPoints" },
                values: new object[] { 1, "Team Alpha", 1, DateTime.SpecifyKind(DateTime.Parse("2023-10-01 12:00:00"), DateTimeKind.Utc), 1500 });
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "TeamName", "CreatedByUserId", "CreatedAt", "TotalPoints" },
                values: new object[] { 2, "Team Beta", 2, DateTime.SpecifyKind(DateTime.Parse("2023-10-02 12:00:00"), DateTimeKind.Utc), 1200 });
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "TeamName", "CreatedByUserId", "CreatedAt", "TotalPoints" },
                values: new object[] { 3, "Team Gamma", 3, DateTime.SpecifyKind(DateTime.Parse("2023-10-03 12:00:00"), DateTimeKind.Utc), 900 });
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "TeamName", "CreatedByUserId", "CreatedAt", "TotalPoints" },
                values: new object[] { 4, "Team Delta", 4, DateTime.SpecifyKind(DateTime.Parse("2023-10-04 12:00:00"), DateTimeKind.Utc), 800 });

            // Update Users with TeamIds (using SQL for efficiency)
            migrationBuilder.Sql(@"
                UPDATE ""Users"" SET ""TeamId"" = 1 WHERE ""UserId"" IN (1, 4, 8, 12, 16, 20);
                UPDATE ""Users"" SET ""TeamId"" = 2 WHERE ""UserId"" IN (2, 5, 9, 13, 17);
                UPDATE ""Users"" SET ""TeamId"" = 3 WHERE ""UserId"" IN (3, 6, 10, 14, 18);
                UPDATE ""Users"" SET ""TeamId"" = 4 WHERE ""UserId"" IN (7, 11, 15, 19);
            ");

            // Challenges
            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "ChallengeId", "Name", "Description", "Category", "Difficulty", "Points", "Flag", "FilePath", "CreatedAt", "IsActive" },
                values: new object[] { 1, "Web Challenge 1", "Find the hidden flag in the website.", 0, 0, 100, "FLAG{web1}", null, DateTime.SpecifyKind(DateTime.Parse("2023-10-01 12:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "ChallengeId", "Name", "Description", "Category", "Difficulty", "Points", "Flag", "FilePath", "CreatedAt", "IsActive" },
                values: new object[] { 2, "Forensics Challenge 1", "Analyze the image to find the flag.", (int)CategoryEnum.Forensics, (int)DifficultyEnum.Easy, 200, "FLAG{forensics1}", "/path/to/file1", DateTime.SpecifyKind(DateTime.Parse("2023-10-02 12:00:00"), DateTimeKind.Utc), true }); // Cast enums to int
            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "ChallengeId", "Name", "Description", "Category", "Difficulty", "Points", "Flag", "FilePath", "CreatedAt", "IsActive" },
                values: new object[] { 3, "Crypto Challenge 1", "Decrypt the message to find the flag.", (int)CategoryEnum.Cryptography, (int)DifficultyEnum.Medium, 300, "FLAG{crypto1}", null, DateTime.SpecifyKind(DateTime.Parse("2023-10-03 12:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "ChallengeId", "Name", "Description", "Category", "Difficulty", "Points", "Flag", "FilePath", "CreatedAt", "IsActive" },
                values: new object[] { 4, "Reverse Engineering Challenge 1", "Reverse the binary to find the flag.", (int)CategoryEnum.ReverseEngineering, (int)DifficultyEnum.Medium, 400, "FLAG{reverse1}", "/path/to/file2", DateTime.SpecifyKind(DateTime.Parse("2023-10-04 12:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "ChallengeId", "Name", "Description", "Category", "Difficulty", "Points", "Flag", "FilePath", "CreatedAt", "IsActive" },
                values: new object[] { 5, "Binary Exploitation Challenge 1", "Exploit the binary to get the flag.", (int)CategoryEnum.BinaryExploitation, (int)DifficultyEnum.Easy, 250, "FLAG{binary1}", null, DateTime.SpecifyKind(DateTime.Parse("2023-10-05 12:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "ChallengeId", "Name", "Description", "Category", "Difficulty", "Points", "Flag", "FilePath", "CreatedAt", "IsActive" },
                values: new object[] { 6, "OSINT Challenge 1", "Use open-source intelligence to find the flag.", (int)CategoryEnum.OSINT, (int)DifficultyEnum.Easy, 150, "FLAG{osint1}", null, DateTime.SpecifyKind(DateTime.Parse("2023-10-06 12:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "ChallengeId", "Name", "Description", "Category", "Difficulty", "Points", "Flag", "FilePath", "CreatedAt", "IsActive" },
                values: new object[] { 7, "Miscellaneous Challenge 1", "Solve the puzzle to find the flag.", (int)CategoryEnum.Miscellaneous, (int)DifficultyEnum.Easy, 100, "FLAG{misc1}", null, DateTime.SpecifyKind(DateTime.Parse("2023-10-07 12:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "ChallengeId", "Name", "Description", "Category", "Difficulty", "Points", "Flag", "FilePath", "CreatedAt", "IsActive" },
                values: new object[] { 8, "Web Challenge 2", "Exploit the web application to find the flag.", (int)CategoryEnum.Web, (int)DifficultyEnum.Easy, 200, "FLAG{web2}", null, DateTime.SpecifyKind(DateTime.Parse("2023-10-08 12:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "ChallengeId", "Name", "Description", "Category", "Difficulty", "Points", "Flag", "FilePath", "CreatedAt", "IsActive" },
                values: new object[] { 9, "Forensics Challenge 2", "Analyze the network traffic to find the flag.", (int)CategoryEnum.Forensics, (int)DifficultyEnum.Medium, 300, "FLAG{forensics2}", "/path/to/file3", DateTime.SpecifyKind(DateTime.Parse("2023-10-09 12:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "ChallengeId", "Name", "Description", "Category", "Difficulty", "Points", "Flag", "FilePath", "CreatedAt", "IsActive" },
                values: new object[] { 10, "Crypto Challenge 2", "Break the encryption to find the flag.", (int)CategoryEnum.Cryptography, (int)DifficultyEnum.Easy, 250, "FLAG{crypto2}", null, DateTime.SpecifyKind(DateTime.Parse("2023-10-10 12:00:00"), DateTimeKind.Utc), true });

            // Submissions
            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "SubmissionId", "ChallengeId", "UserId", "TeamId", "SubmittedFlag", "SubmittedAt", "IsCorrect" },
                values: new object[] { 1, 1, 1, 1, "FLAG{web1}", DateTime.SpecifyKind(DateTime.Parse("2023-10-01 13:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "SubmissionId", "ChallengeId", "UserId", "TeamId", "SubmittedFlag", "SubmittedAt", "IsCorrect" },
                values: new object[] { 2, 2, 2, 2, "FLAG{forensics1}", DateTime.SpecifyKind(DateTime.Parse("2023-10-02 14:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "SubmissionId", "ChallengeId", "UserId", "TeamId", "SubmittedFlag", "SubmittedAt", "IsCorrect" },
                values: new object[] { 3, 3, 2, 2, "FLAG{wrongflag}", DateTime.SpecifyKind(DateTime.Parse("2023-10-03 15:00:00"), DateTimeKind.Utc), false });
            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "SubmissionId", "ChallengeId", "UserId", "TeamId", "SubmittedFlag", "SubmittedAt", "IsCorrect" },
                values: new object[] { 4, 4, 4, 1, "FLAG{reverse1}", DateTime.SpecifyKind(DateTime.Parse("2023-10-04 16:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "SubmissionId", "ChallengeId", "UserId", "TeamId", "SubmittedFlag", "SubmittedAt", "IsCorrect" },
                values: new object[] { 5, 5, 5, 2, "FLAG{binary1}", DateTime.SpecifyKind(DateTime.Parse("2023-10-05 17:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "SubmissionId", "ChallengeId", "UserId", "TeamId", "SubmittedFlag", "SubmittedAt", "IsCorrect" },
                values: new object[] { 6, 6, 6, 3, "FLAG{osint1}", DateTime.SpecifyKind(DateTime.Parse("2023-10-06 18:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "SubmissionId", "ChallengeId", "UserId", "TeamId", "SubmittedFlag", "SubmittedAt", "IsCorrect" },
                values: new object[] { 7, 7, 7, 4, "FLAG{misc1}", DateTime.SpecifyKind(DateTime.Parse("2023-10-07 19:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "SubmissionId", "ChallengeId", "UserId", "TeamId", "SubmittedFlag", "SubmittedAt", "IsCorrect" },
                values: new object[] { 8, 8, 8, 1, "FLAG{web2}", DateTime.SpecifyKind(DateTime.Parse("2023-10-08 20:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "SubmissionId", "ChallengeId", "UserId", "TeamId", "SubmittedFlag", "SubmittedAt", "IsCorrect" },
                values: new object[] { 9, 9, 9, 2, "FLAG{forensics2}", DateTime.SpecifyKind(DateTime.Parse("2023-10-09 21:00:00"), DateTimeKind.Utc), true });
            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "SubmissionId", "ChallengeId", "UserId", "TeamId", "SubmittedFlag", "SubmittedAt", "IsCorrect" },
                values: new object[] { 10, 10, 10, 3, "FLAG{crypto2}", DateTime.SpecifyKind(DateTime.Parse("2023-10-10 22:00:00"), DateTimeKind.Utc), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Users");
            migrationBuilder.Sql("DELETE FROM Teams");
            migrationBuilder.Sql("DELETE FROM Challenges");
            migrationBuilder.Sql("DELETE FROM Submissions");
        }
    }
}
