using System.Linq.Expressions;
using CTF_Platform_dotnet.Models;

namespace CTF_Platform_dotnet.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector, bool ascending = true);
    Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

}
