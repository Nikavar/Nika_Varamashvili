using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Web.Models
{
    public class TabViewModel
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? TabName { get; set; }
		public string? Description { get; set; }

		[ForeignKey("ParentId")]
        public List<TabViewModel>? Child { get; set; }
    }
}
