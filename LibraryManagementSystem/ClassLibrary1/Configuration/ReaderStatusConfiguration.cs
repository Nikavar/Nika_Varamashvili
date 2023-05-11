using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Configuration
{
    public class ReaderStatusConfiguration : EntityTypeConfiguration<ReaderStatus>
    {
        public ReaderStatusConfiguration()
        {
            ToTable("ReaderStatuses");
            // სტატიკური ცხრილებისთვის საჭიროა თუ არა კონფიგურაციის განსაზღვრა?

            //Property(rs => rs.);
        }
    }
}
