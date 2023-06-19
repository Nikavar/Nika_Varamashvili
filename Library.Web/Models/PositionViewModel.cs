using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
	public class PositionViewModel
	{
		[Key]
		public int ID { get; set; }
		public string? PositionName { get; set; }
		public double? Salary { get; set; }
		public DateTime? StartWorkingDate { get; set; }
		public int LogID { get; set; }
	}
}
