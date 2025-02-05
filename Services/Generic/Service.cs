using CTF_Platform_dotnet.Repositories;
using static CTF_Platform_dotnet.Services.Generic.IService;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CTF_Platform_dotnet.Services.Generic
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetOrderedAsync<TKey>(Expression<Func<T, TKey>> keySelector, bool ascending = true)
        {
            var query = _repository.OrderBy(keySelector, ascending);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return (IEnumerable<T>)await _repository.GetPagedAsync(pageNumber, pageSize);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return await _repository.CountAsync(predicate);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
