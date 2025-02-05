using System.Linq.Expressions;
using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.Repositories;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace CTF_Platform_dotnet.Services
{
    public class TeamService : ITeamService
    {
        private readonly IRepository<Team> _teamRepository;

        public TeamService(IRepository<Team> teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _teamRepository.GetAll().ToListAsync();
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            return await _teamRepository.GetByIdAsync(id);
        }

        public async Task AddTeamAsync(Team team)
        {
            await _teamRepository.AddAsync(team);
        }

        public async Task UpdateTeamAsync(Team team)
        {
            await _teamRepository.UpdateAsync(team);
        }

        public async Task DeleteTeamAsync(int id)
        {
            await _teamRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Team>> GetTeamsByPredicateAsync(Expression<Func<Team, bool>> predicate)
        {
            return await _teamRepository.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetTeamsOrderedAsync<TKey>(Expression<Func<Team, TKey>> keySelector, bool ascending = true)
        {
            return await _teamRepository.OrderBy(keySelector, ascending).ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetPagedTeamsAsync(int pageNumber, int pageSize)
        {
            return (IEnumerable<Team>)await _teamRepository.GetPagedAsync(pageNumber, pageSize);
        }

        public async Task<int> CountTeamsAsync(Expression<Func<Team, bool>>? predicate = null)
        {
            return await _teamRepository.CountAsync(predicate);
        }

        public async Task<bool> TeamExistsAsync(Expression<Func<Team, bool>> predicate)
        {
            return await _teamRepository.ExistsAsync(predicate);
        }
    }
}
