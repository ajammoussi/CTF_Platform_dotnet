using CTF_Platform_dotnet.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CTF_Platform_dotnet.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<IEnumerable<User>> GetUsersByPredicateAsync(Expression<Func<User, bool>> predicate);
        Task<IEnumerable<User>> GetUsersOrderedAsync<TKey>(Expression<Func<User, TKey>> keySelector, bool ascending = true);
        Task<IEnumerable<User>> GetPagedUsersAsync(int pageNumber, int pageSize);
        Task<int> CountUsersAsync(Expression<Func<User, bool>>? predicate = null);
        Task<bool> UserExistsAsync(Expression<Func<User, bool>> predicate);
    }
}