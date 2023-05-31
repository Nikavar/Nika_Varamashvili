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
        Task AddUserAsync(User entity);
        Task UpdateUserAsync(User entity);
        Task DeleteUserAsync(User entity);
        Task DeleteManyUsersAsync(Expression<Func<User, bool>> filter);
        Task SaveUserAsync();
        

        // Login User
        Task<User> LoginUserAsync(string userName, string password);
        Task<User> CheckUserByPasswordAsync(string password);
        Task<User> CheckUserByMailAsync(string mail);

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

        public async Task AddUserAsync(User entity)
        {
            await _userRepository.AddAsync(entity);
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

        public async Task<User> CheckUserByPasswordAsync(string password)
        {
            return await _userRepository.CheckUserByPasswordAsync(password);
        }

        public async Task<User> CheckUserByMailAsync(string mail)
        {
            return await _userRepository.CheckUserByMailAsync(mail);
        }

        public async Task SaveUserAsync()
        {
            await _userRepository.SaveAsync();
        }
    }
}
