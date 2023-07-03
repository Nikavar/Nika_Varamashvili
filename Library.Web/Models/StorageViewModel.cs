using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class StorageViewModel
    {
        [Key]
        public int Id { get; set; }
        public int ClosetNumber { get; set; }
        public int ShelfNumber { get; set; }
    }
}
