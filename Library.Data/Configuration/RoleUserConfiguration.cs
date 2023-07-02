using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Configuration
{
    public class RoleUserConfiguration : IEntityTypeConfiguration<RoleUser>
    {
        public void Configure(EntityTypeBuilder<RoleUser> builder)
        {
            builder.ToTable("RoleUsers");

            builder.Property(ur => ur.UserID)
                .HasColumnName("User_Id");

            builder.Property(ur => ur.RoleID)
                .HasColumnName("Role_Id");

            // relations

            builder.HasOne(u => u.User)
                .WithMany(ur => ur.UserRoles);

            builder.HasOne(r => r.Role)
                .WithMany(ur => ur.RoleUsers);
        

        }
    }
}
