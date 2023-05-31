using Library.Data.Infrastructure;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<User> CheckUserByMailAsync(string mail)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.UserName == mail);
        }

        public async Task<User> CheckUserByPasswordAsync(string password)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.Password == password);
        }

        public async Task<User> LoginUserAsync(string userName, string password)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
        }

        public Task LogoutUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterUser(User user)
        {
            var result = await DbContext.Users.AddAsync(user);
        }

        public override Task UpdateAsync(User entity)
        {
            return base.UpdateAsync(entity);
        }

        public override Task SaveAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }

    public interface IUserRepository : IBaseRepository<User> 
    {
        Task<User> LoginUserAsync(string userName, string password);    
        Task LogoutUser(User user);
        Task RegisterUser(User user);

        //---------------------------------------------------
        Task<User> CheckUserByPasswordAsync(string password);
        Task<User> CheckUserByMailAsync(string mail);
    }
}
