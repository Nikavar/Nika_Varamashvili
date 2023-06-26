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
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");


            builder.Property(a => a.FirstName)
                .HasMaxLength(30)
                .IsUnicode(true);

            builder.Property(a => a.LastName)
                .HasMaxLength(30)
                .IsUnicode(true);

            builder.Property(a => a.DoB)
                .HasColumnType("date");

            builder.Property(u => u.LogID)
                .HasColumnName("LogID");
        }
    }
}
