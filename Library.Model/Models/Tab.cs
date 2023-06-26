using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Tab
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? TabName { get; set; }
        public string? Description { get; set; }

        [ForeignKey("ParentId")]
        public List<Tab> Child { get; set; }
    }
}
