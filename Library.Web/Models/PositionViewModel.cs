using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
	public class PositionViewModel
	{
		[Key]
		public int ID { get; set; }

        [Required(ErrorMessage = "Please enter position name!")]
        public string? PositionName { get; set; }
		public double? Salary { get; set; }
		public DateTime? StartWorkingDate { get; set; }
	}
}
