using Library.Data.Infrastructure;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DbFactory dbFactory) : base(dbFactory) 
        { 
        
        
        }

        public override User GetById(int id)
        {
            var user = this.DbContext.Users.Where(u => u.UserID == id).FirstOrDefault();
            return user ?? throw new NotImplementedException();
        }

        // Authentification Users
        public async Task<User> AuthenticateUser(string username, string password)
        {
            var succeded = await this.DbContext.Users.FirstOrDefaultAsync(authUser =>
            authUser.UserName == username && authUser.Password == password);
            return succeded ?? throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> getUser()
        {
            throw new NotImplementedException();
        }
    }

    public interface IUserRepository : IRepository<User> 
    {
        Task<IEnumerable<User>> getUser();
        Task<User> AuthenticateUser(string userName, string password);    
    }
}
