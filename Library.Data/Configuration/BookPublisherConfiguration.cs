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
    public class BookPublisherConfiguration : IEntityTypeConfiguration<BookPublisher>
    {
        public void Configure(EntityTypeBuilder<BookPublisher> builder)
        {
            builder.ToTable("BookPublishers");

            builder.HasKey(bp => bp.Id);

            builder.Property(bp => bp.PublisherId)
                .HasColumnName("Publisher_Id");

            builder.Property(bp => bp.BookId)
                .HasColumnName("Book_Id");

            builder.Property(bp => bp.LogID)
                .HasColumnName("LogID");

            // relations
            builder.HasOne(bp => bp.Book)
                .WithMany(bp => bp.BookPublishers);

            builder.HasOne(bp => bp.Publisher)
                .WithMany(bp => bp.BookPublishers);
        }
    }
}
