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
    public class ReaderStatusConfiguration : IEntityTypeConfiguration<ReaderStatus>
    {
        public void Configure(EntityTypeBuilder<ReaderStatus> builder)
        {
            builder.ToTable("ReaderStatuses");
        }
           
    }
}
