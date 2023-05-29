using Library.Model.Models;

namespace Library.Web.Models
{
    public class Position
    {
        public string? PositionName { get; set; }
        public double? Salary { get; set; }
        public DateTime? StartWorkingDate { get; set; }
        public int? LogID { get; set; }
    }
}
