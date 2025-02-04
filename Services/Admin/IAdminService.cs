using CTF_Platform_dotnet.DTOs;
using CTF_Platform_dotnet.Models;

public interface IAdminService{
    Task<(ChallengeSolveDto? MostSolved, ChallengeSolveDto? LeastSolved)> GetMostAndLeastSolvedChallengesAsync();
    Task<PagedResponse<TeamDto>> GetTeamScoreboardAsync(int pageNumber, int pageSize);
    Task<PagedResponse<UserDto>> GetUserScoreboardAsync(int pageNumber, int pageSize); 
}