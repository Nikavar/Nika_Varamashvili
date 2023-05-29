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
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        public async Task LoginUserAsync(User user)
        {
            var result = await DbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == user.UserName && u.Password == user.Password);
        }

        public Task LogoutUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterUser(User user)
        {
            var result = await this.DbContext.Users.AddAsync(user);
        }

        public override Task UpdateAsync(User entity)
        {
            return base.UpdateAsync(entity);
        }
    }

    public interface IUserRepository : IBaseRepository<User> 
    {
        Task LoginUserAsync(User user);    
        Task LogoutUser(User user);
        Task RegisterUser(User user);
    }
}
