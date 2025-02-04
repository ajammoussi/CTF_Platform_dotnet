using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CTF_Platform_dotnet.Services.Generic
{
    public interface IService
    {
        public interface IService<T> where T : class
        {
            // Get all entities
            Task<IEnumerable<T>> GetAllAsync();

            // Get an entity by ID
            Task<T> GetByIdAsync(int id);

            // Add a new entity
            Task AddAsync(T entity);

            // Update an existing entity
            Task UpdateAsync(T entity);

            // Delete an entity by ID
            Task DeleteAsync(int id);

            // Get entities based on a predicate (filter)
            Task<IEnumerable<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate);

            // Get entities ordered by a key
            Task<IEnumerable<T>> GetOrderedAsync<TKey>(Expression<Func<T, TKey>> keySelector, bool ascending = true);

            // Get paged entities
            Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);

            // Count entities (optionally filtered by a predicate)
            Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

            // Check if an entity exists based on a predicate
            Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        }
    }
}