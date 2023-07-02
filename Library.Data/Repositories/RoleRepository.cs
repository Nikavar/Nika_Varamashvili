using Library.Data.Infrastructure;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }

        public Role GetRoleByNameAsync(string roleName)
        {
            var res = dbSet.FirstOrDefault(r => r.RoleName.Equals(roleName));
            return res;
        }
    }

    public interface IRoleRepository : IBaseRepository<Role>
    {
        Role GetRoleByNameAsync(string roleName);
    }
}
