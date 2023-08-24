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
    public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.ToTable("BookAuthors");

            builder.HasKey(ba => ba.Id);

            builder.Property(ba => ba.AuthorId)
                .HasColumnName("AuthorId");

            builder.Property(ba => ba.BookId)
                .HasColumnName("BookId");

            // relations
            builder.HasOne(ba => ba.book)
                .WithMany(bp => bp.BookAuthors);

            builder.HasOne(bp => bp.author)
                .WithMany(bp => bp.BookAuthors);
        }
    }
}
