using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using static CTF_Platform_dotnet.Services.Generic.IService;

namespace CTF_Platform_dotnet.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAll().ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersByPredicateAsync(Expression<Func<User, bool>> predicate)
        {
            return await _userRepository.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersOrderedAsync<TKey>(Expression<Func<User, TKey>> keySelector, bool ascending = true)
        {
            return await _userRepository.OrderBy(keySelector, ascending).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetPagedUsersAsync(int pageNumber, int pageSize)
        {
            return (IEnumerable<User>)await _userRepository.GetPagedAsync(pageNumber, pageSize);
        }

        public async Task<int> CountUsersAsync(Expression<Func<User, bool>>? predicate = null)
        {
            return await _userRepository.CountAsync(predicate);
        }

        public async Task<bool> UserExistsAsync(Expression<Func<User, bool>> predicate)
        {
            return await _userRepository.ExistsAsync(predicate);
        }

        //public async Task<User> GetCurrentUser()
        //{
        //    // Get the user's ID from the claims
        //    var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        throw new UnauthorizedAccessException("User is not authenticated.");
        //    }

        //    // Fetch the user from the database
        //    var user = await GetUserByIdAsync(int.Parse(userId));

        //    if (user == null)
        //    {
        //        throw new InvalidOperationException("User not found.");
        //    }

        //    return user;
        //}
    }
}
