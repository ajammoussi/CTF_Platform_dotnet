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

    public IQueryable<T> GetAll() => _dbSet.AsQueryable(); //more efficient than returning IEnumerable 

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
    public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }
    public IQueryable<T> OrderBy<TKey>(
    Expression<Func<T, TKey>> keySelector, bool ascending = true)
    {
        return ascending 
            ? _dbSet.OrderBy(keySelector) 
            : _dbSet.OrderByDescending(keySelector);
    }

    public async Task<PagedResponse<T>> GetPagedAsync(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0) throw new ArgumentException("Page number must be greater than 0.");
        if (pageSize <= 0 || pageSize > 50) throw new ArgumentException("Page size must be between 1 and 50.");
        var totalItems = await _dbSet.CountAsync();
        var items = await _dbSet
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PagedResponse<T>(items, pageNumber, pageSize, totalItems);
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
