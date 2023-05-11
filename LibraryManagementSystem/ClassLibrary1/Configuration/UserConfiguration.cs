using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
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
            Property(u => u.UserName).IsRequired();
            Property(u => u.Password).IsRequired();
            Property(u => u.StaffReaderID).IsRequired();
            Property(u => u.OperationHistoryID).IsRequired();
        }

    }
}
