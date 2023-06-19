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

        public User CheckUserByMail(string mail)
        {
            return dbSet.FirstOrDefault(u => u.UserName == mail);
        }

        public async Task<User> CheckUserByPasswordAsync(string password)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.Password == password);
        }

        public User LoginUser(string userName, string password)
        {
            return dbSet.Where(u => u.UserName == userName && u.Password == password).FirstOrDefault();
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

		public async Task<User> GetLastUser(User newUser)
		{
            return await dbSet.LastOrDefaultAsync();
		}
	}

    public interface IUserRepository : IBaseRepository<User> 
    {
        User LoginUser(string userName, string password);    
        Task LogoutUser(User user);
        Task RegisterUser(User user);

        //---------------------------------------------------
        Task<User> CheckUserByPasswordAsync(string password);
        User CheckUserByMail(string mail);
		Task<User> GetLastUser(User newUser);
	}
}
