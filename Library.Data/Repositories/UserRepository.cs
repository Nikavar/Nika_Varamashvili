using Library.Data.Infrastructure;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {

        }
        public async Task LoginUserAsync(User user)
        {
            var result = await _dbContext.AddAsync(user);
            _dbContext.SaveChanges();
        }

        public Task LogoutUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterUser(User user)
        {
           await _dbContext.AddAsync(user);
           _dbContext.SaveChanges();
        }
    }

    public interface IUserRepository : IBaseRepository<User> 
    {
        Task LoginUserAsync(User user);    
        Task LogoutUser(User user);
        Task RegisterUser(User user);

    }
}
