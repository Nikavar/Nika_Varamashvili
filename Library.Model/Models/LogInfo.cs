using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Model.Models
{
    public class LogInfo
    {
        [Key]
        public int LogID { get; set; }
        public string? TableName { get; set; }
        public string? DateCreated { get; set; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        public string? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int? UserID { get; set; }
        public int? EntityID { get; set; }
        public string? LogStatus { get; set; }
        public string? LogContent { get; set; }

    }
}
