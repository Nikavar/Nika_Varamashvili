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
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");

            builder.Property(ur => ur.UserID)
                   .IsRequired();
            builder.Property(ur => ur.RoleID)
                   .IsRequired();
            builder.Property(ur => ur.LogID)
                   .IsRequired();

            // relations

            builder.HasOne(u => u.User)
                .WithMany(ur => ur.UserRoles);

            builder.HasOne(l => l.Logs)
                .WithMany(ur => ur.UserRoles);
        }
    }
}
