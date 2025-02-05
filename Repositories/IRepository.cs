using System.Linq.Expressions;
using CTF_Platform_dotnet.Models;

namespace CTF_Platform_dotnet.Repositories;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector, bool ascending = true);
    Task<PagedResponse<T>> GetPagedAsync(int pageNumber, int pageSize);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task DeleteAsync(T entity);
}

