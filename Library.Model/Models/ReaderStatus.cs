using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class ReaderStatus
    {
        [Key]
        public int ReaderStatusID { get; set; }
        public string? ReaderStat { get; set; }

        // relations
        public ICollection<StaffReader>? StaffReaders { get; set; }
    }
}
