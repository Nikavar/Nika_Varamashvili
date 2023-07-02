using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Publisher = Library.Model.Models.Publisher;

namespace Library.Data.Configuration
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("Publishers");

            builder.Property(p => p.PublisherName)
                .IsUnicode(true);
            builder.Property(p => p.Address)
                .IsUnicode (true);
            builder.Property(p => p.Email)
                .IsUnicode (false);

            // relation


        }
    }
}
