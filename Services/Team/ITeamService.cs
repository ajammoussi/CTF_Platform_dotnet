using System.Linq.Expressions;
using CTF_Platform_dotnet.Models;

namespace CTF_Platform_dotnet.Services
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team> GetTeamByIdAsync(int id);
        Task AddTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task DeleteTeamAsync(int id);
        Task<IEnumerable<Team>> GetTeamsByPredicateAsync(Expression<Func<Team, bool>> predicate);
        Task<IEnumerable<Team>> GetTeamsOrderedAsync<TKey>(Expression<Func<Team, TKey>> keySelector, bool ascending = true);
        Task<IEnumerable<Team>> GetPagedTeamsAsync(int pageNumber, int pageSize);
        Task<int> CountTeamsAsync(Expression<Func<Team, bool>>? predicate = null);
        Task<bool> TeamExistsAsync(Expression<Func<Team, bool>> predicate);
    }
}
