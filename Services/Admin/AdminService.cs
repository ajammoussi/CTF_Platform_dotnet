using AutoMapper;
using AutoMapper.QueryableExtensions;
using CTF_Platform_dotnet.DTOs;
using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.Repositories;
using Microsoft.EntityFrameworkCore;

public class AdminService : IAdminService
{
    private readonly IRepository<Submission> _submissionRepository;
    private readonly IRepository<Team> _teamRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public AdminService(IRepository<Submission> submissionRepository, IRepository<Team> teamRepository, IRepository<User> userRepository, IMapper mapper)
    {
        _submissionRepository = submissionRepository;
        _teamRepository = teamRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<(ChallengeSolveDto? MostSolved, ChallengeSolveDto? LeastSolved)> GetMostAndLeastSolvedChallengesAsync()
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



    public async Task<PagedResponse<TeamDto>> GetTeamScoreboardAsync(int pageNumber, int pageSize)
    {

        var totalItems = await _teamRepository.CountAsync();

        var teams = await _teamRepository.GetAll()
            .OrderByDescending(t => t.TotalPoints)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<TeamDto>(_mapper.ConfigurationProvider) // using AutoMapper
            .ToListAsync();
        return new PagedResponse<TeamDto>(teams, pageNumber, pageSize, totalItems);
    }


    public async Task<PagedResponse<UserDto>> GetUserScoreboardAsync(int pageNumber, int pageSize)
    {

        var totalItems = await _userRepository.CountAsync();

        var users = await _userRepository.GetAll()
            .OrderByDescending(u => u.Points)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return new PagedResponse<UserDto>(users, pageNumber, pageSize, totalItems);
    }

}