using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.Repositories;
using Microsoft.EntityFrameworkCore;

public class AdminService : IAdminService
{
    private readonly IRepository<Submission> _submissionRepository;
    private readonly IRepository<Team> _teamRepository;
    private readonly IRepository<User> _userRepository;

    public AdminService(IRepository<Submission> submissionRepository, IRepository<Team> teamRepository, IRepository<User> userRepository)
    {
        _submissionRepository = submissionRepository;
        _teamRepository = teamRepository;
        _userRepository = userRepository;
    }

    public async Task<(ChallengeSolveDto? MostSolved, ChallengeSolveDto? LeastSolved)> GetMostAndLeastSolvedChallengesAsync()
    {
        try
        {
            var stats = await _submissionRepository.GetAll()
                .Where(s => s.IsCorrect == true)
                .GroupBy(s => new { s.ChallengeId, s.Challenge.Name })
                .Select(g => new ChallengeSolveDto
                {
                    ChallengeName = g.Key.Name,
                    Count = g.Count()
                })
                .ToListAsync();

            return (
                stats.MaxBy(x => x.Count),
                stats.MinBy(x => x.Count)
            );
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<PagedResponse<Team>> GetTeamScoreboardAsync(int pageNumber, int pageSize)
    {
        try
        {
            var totalItems = await _teamRepository.CountAsync();

                var teams = await _teamRepository.GetAll()
                    .OrderByDescending(t => t.TotalPoints)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            return new PagedResponse<Team>(teams, pageNumber, pageSize, totalItems);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public async Task<PagedResponse<User>> GetUserScoreboardAsync(int pageNumber, int pageSize)
    {
        try
        {
            var totalItems = await _userRepository.CountAsync();

            var users = await _userRepository.GetAll()
                .OrderByDescending(u => u.Points)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedResponse<User>(users, pageNumber, pageSize, totalItems);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}