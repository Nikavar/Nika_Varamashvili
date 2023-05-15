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
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");
            Property(u => u.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsRequired();
            Property(u => u.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
            Property(u => u.StaffReaderID)
                .IsRequired();
            Property(u => u.LogID)
                .IsRequired();

            HasRequired<StaffReader>(sr => sr.StaffReader)
                .WithMany(u => u.Users);

            HasRequired<LogInfo>(l => l.Logs)
                .WithMany(u => u.Users);
        }
    }
}
