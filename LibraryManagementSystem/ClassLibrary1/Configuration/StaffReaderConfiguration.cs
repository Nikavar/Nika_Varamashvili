using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Configuration
{
    public class StaffReaderConfiguration : EntityTypeConfiguration<StaffReader>
    {
        public StaffReaderConfiguration()
        {
            ToTable("StaffReaders");
            Property(s => s.FirstName).IsRequired().HasMaxLength(30);
            Property(s => s.LastName).IsRequired().HasMaxLength(50);
            Property(s => s.Email).IsRequired();
            Property(s => s.PhoneNumber).IsRequired();
            Property(s => s.OperationHistoryID).IsRequired();
        }
    }
}
