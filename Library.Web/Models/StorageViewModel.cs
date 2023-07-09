using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class StorageViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ClosetNumber { get; set; }
        [Required]
        public int ShelfNumber { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
