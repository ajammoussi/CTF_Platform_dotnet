using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CTF_Platform_dotnet.Models;

namespace CTF_Platform_dotnet.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly CTFContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(CTFContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
    public async Task<IEnumerable<T>> OrderBy<TKey>(
    Expression<Func<T, TKey>> keySelector, bool ascending = true)
    {
        if (ascending)
        {
            return await _dbSet.OrderBy(keySelector).ToListAsync();
        }
        else
        {
            return await _dbSet.OrderByDescending(keySelector).ToListAsync();
        }
    }
    public async Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _dbSet
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountAsync (Expression<Func<T,bool>>? predicate = null)
    {
        if (predicate ==null)
        {
            return await _dbSet.CountAsync();
        }
        return await _dbSet.CountAsync(predicate);
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

}
