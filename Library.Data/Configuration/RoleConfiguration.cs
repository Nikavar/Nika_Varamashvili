using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.Property(r => r.RoleName)
                .HasMaxLength(50);

            builder.Property(r => r.LogID)
                .HasColumnName("LogID");

            // relations Role => LogInfo
            builder.HasOne(l => l.Log)
                .WithMany(r => r.Roles);       
        }
    }
}
