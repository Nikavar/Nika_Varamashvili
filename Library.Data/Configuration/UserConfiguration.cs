using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.Property(u => u.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsRequired();
            builder.Property(u => u.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
            builder.Property(u => u.StaffReaderID)
                .IsRequired();
            builder.Property(u => u.LogID)
                .IsRequired();

            builder.HasOne(sr => sr.StaffReader)
                .WithMany(u => u.Users);

            builder.HasOne(l => l.Logs)
                .WithMany(u => u.Users);
        }

    }
}
