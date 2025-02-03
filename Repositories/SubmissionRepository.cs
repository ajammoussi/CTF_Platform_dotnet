using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CTF_Platform_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace CTF_Platform_dotnet.Repositories
{
    public class SubmissionRepository : IRepository<Submission>
    {
        private readonly CTFContext _context;
        private readonly DbSet<Submission> _dbSet;

        public SubmissionRepository(CTFContext context)
        {
            _context = context;
            _dbSet = context.Set<Submission>();
        }

        public IQueryable<Submission> GetAll() => _dbSet;

        public async Task<Submission?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(Submission entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Submission entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Submission> Where(Expression<Func<Submission, bool>> predicate) => _dbSet.Where(predicate);

        public IQueryable<Submission> OrderBy<TKey>(Expression<Func<Submission, TKey>> keySelector, bool ascending = true)
        {
            return ascending ? _dbSet.OrderBy(keySelector) : _dbSet.OrderByDescending(keySelector);
        }

        public async Task<PagedResponse<Submission>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var totalItems = await _dbSet.CountAsync();
            var items = await _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResponse<Submission>(items, pageNumber, pageSize, totalItems);
        }

        public async Task<int> CountAsync(Expression<Func<Submission, bool>>? predicate = null)
        {
            return predicate != null ? await _dbSet.CountAsync(predicate) : await _dbSet.CountAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Submission, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task DeleteAsync(Submission entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
