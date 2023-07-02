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
        Task<IEnumerable<User>> GetManyUsersAsync(Expression<Func<User, bool>> filter);
        Task<User> GetUserByIdAsync(int id);
        Task<User> AddUserAsync(User entity);
        Task UpdateUserAsync(User entity);
        Task DeleteUserAsync(User entity);
        Task DeleteManyUsersAsync(Expression<Func<User, bool>> filter);
        Task SaveUserAsync();
        

        // Login & Register User
        Task<User> LoginUserAsync(string userName, string password);
        Task LogoutUserAsync(User user);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStaffReaderRepository _staffReaderRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository userRepository, IStaffReaderRepository staffReader, IUnitOfWork unitOfWork)
        {
            this._userRepository = userRepository;
            this._staffReaderRepository = staffReader;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<IEnumerable<User>> GetManyUsersAsync(Expression<Func<User, bool>> filter)
        {
            return await _userRepository.GetManyAsync(filter);
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> AddUserAsync(User entity)
        {
            return await _userRepository.AddAsync(entity);
        }

        public async Task UpdateUserAsync(User entity)
        {
            await _userRepository.UpdateAsync(entity);
        }

        public async Task DeleteUserAsync(User user)
        {
            await _userRepository.DeleteAsync(user);
        }

        public async Task DeleteManyUsersAsync(Expression<Func<User, bool>> filter)
        {
            await _userRepository.DeleteManyAsync(filter);
        }

        public async Task<User> LoginUserAsync(string userName, string password)
        {
            return await _userRepository.LoginUserAsync(userName,password);
        }

        public async Task SaveUserAsync()
        {
            await _userRepository.SaveAsync();
        }

        public async Task LogoutUserAsync(User user)
        {
           await _userRepository.LogoutUserAsync(user);
        }
    }
}
