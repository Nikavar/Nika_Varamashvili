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
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards");

            builder.Property(c => c.BookLimit)
                   .IsRequired();
            builder.Property(c => c.ValidFrom)
                   .HasColumnType("datetime")
                   .IsRequired();
            builder.Property(c => c.ValidTo)
                    .HasColumnType("datetime")
                    .IsRequired();
            builder.Property(c => c.ReaderID)
                   .IsRequired();

            // relations
            builder.HasOne(sr => sr.StaffReader)
                   .WithMany(c => c.Cards);
        }       
    }
}
