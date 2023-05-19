using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Card
    {
        public int CardID { get; set; }
        public int BookLimit { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int ReaderID { get; set; }
        public int LogID { get; set; }

        // Relations
        public StaffReader StaffReader { get; set; }
        public LogInfo Logs { get; set; }
    }
}
