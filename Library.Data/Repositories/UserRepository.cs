using Library.Data.Infrastructure;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

		public async Task<User> GetUserByCredentialsAsync(Expression<Func<User, bool>> filter)
		{
            return await dbSet.Where(filter).FirstOrDefaultAsync();
		}

		public async Task<User> LoginUserAsync(string username, string password)
        {
            return await dbSet.Where(u => u.Email == username && u.Password == password && u.IsConfirmed == true).FirstOrDefaultAsync();
		}

		public Task LogoutUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterUserAsync(User user)
        {
            await dbSet.AddAsync(user);
            await dataContext.SaveChangesAsync();
        }

        public override Task SaveAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }

    public interface IUserRepository : IBaseRepository<User> 
    {
        Task<User> LoginUserAsync(string userName, string password);    
        Task LogoutUserAsync(User user);
        Task RegisterUserAsync(User user);
        Task<User> GetUserByCredentialsAsync(Expression<Func<User,bool>> filter);
	}
}
