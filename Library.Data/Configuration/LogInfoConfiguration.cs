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
    public class LogInfoConfiguration : IEntityTypeConfiguration<LogInfo>
    {
        public void Configure(EntityTypeBuilder<LogInfo> builder)
        {

            builder.ToTable("LogInfo");

            builder.Property(l => l.TableName)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(l => l.DateDeleted)
                .HasColumnType("date")
            .HasColumnName("DateDeleted");


            builder.Property(l => l.UserID)
                .HasColumnName("UserId");
        }
    }
}

