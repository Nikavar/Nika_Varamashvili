using Library.Model.Models;
using Library.Data.Infrastructure;
using Library.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetManyUsersAsync(Expression<Func<User, bool>> filter = null,
                                          Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null,
                                          int? top = null,
                                          int? skip = null,
                                          params string[] includeProperties);
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User entity);
        Task UpdateUserAsync(User entity);
        Task DeleteUserAsync(int id);
        Task DeleteManyUsersAsync(Expression<Func<User, bool>> filter);


        // Login User
        Task LoginUserAsync(User user);

    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<IEnumerable<User>> GetManyUsersAsync(Expression<Func<User, bool>> filter = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, int? top = null, int? skip = null, params string[] includeProperties)
        {
            return await _userRepository.GetManyAsync(filter, orderBy, top, skip, includeProperties);
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task AddUserAsync(User entity)
        {
            await _userRepository.AddAsync(entity);
        }

        public async Task UpdateUserAsync(User entity)
        {
            await _userRepository.UpdateAsync(entity);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task DeleteManyUsersAsync(Expression<Func<User, bool>> filter)
        {
            await _userRepository.DeleteManyAsync(filter);
        }

        public async Task LoginUserAsync(User user)
        {
            await _userRepository.LoginUserAsync(user);
        }
    }
}
