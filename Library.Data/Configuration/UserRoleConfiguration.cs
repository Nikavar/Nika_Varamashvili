using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Configuration
{
    public class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration() 
        {
            ToTable("UserRoles");
            Property(ur => ur.UserID)
                .IsRequired();
            Property(ur => ur.RoleID)                        
                .IsRequired();
            Property(ur => ur.LogID)
                .IsRequired();

            HasRequired<User>(u => u.User)
                .WithMany(ur => ur.UserRoles);

            HasRequired<LogInfo>(l => l.Logs)
                .WithMany(ur => ur.UserRoles);
        }
    }
}
