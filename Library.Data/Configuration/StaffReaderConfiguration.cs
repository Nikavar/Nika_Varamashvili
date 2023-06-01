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
    public class StaffReaderConfiguration : IEntityTypeConfiguration<StaffReader>
    {
        public void Configure(EntityTypeBuilder<StaffReader> builder)
        {
            builder.ToTable("StaffReaders");
            builder.Property(s => s.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(s => s.LastName).IsRequired().HasMaxLength(50);
            //builder.Property(s => s.Email).IsRequired();
            //builder.Property(s => s.PhoneNumber).IsRequired();
            //builder.Property(s => s.LogID).IsRequired();


            builder.HasOne(p => p.Position)
                .WithMany(st => st.StaffReaders);

            builder.HasOne(l => l.Log)
                .WithMany(st => st.StaffReaders);

            builder.HasOne(rs => rs.ReaderStatus)
                .WithMany(rs => rs.StaffReaders);

        }
    }
}
