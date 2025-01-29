using CTF_Platform_dotnet.Models;

public interface IAdminService{
    Task<(ChallengeSolveDto? MostSolved, ChallengeSolveDto? LeastSolved)> GetMostAndLeastSolvedChallengesAsync();
    Task<PagedResponse<Team>> GetTeamScoreboardAsync(int pageNumber, int pageSize);
    Task<PagedResponse<User>> GetUserScoreboardAsync(int pageNumber, int pageSize); 
}