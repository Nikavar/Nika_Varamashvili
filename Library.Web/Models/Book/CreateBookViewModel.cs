using Library.Model.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.Book
{
    public class CreateBookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the Book Title")]
        public string? Title { get; set; }
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile? imgfile { get; set; }

		[Required(ErrorMessage = "Please choose the authors")]
        public List<string>? Authors { get; set; }

        [Required(ErrorMessage = "Please choose the publishers")]
        public List<string>? Publishers { get; set; }

        [Required(ErrorMessage = "Please choose the languages")]
        public List<string>? Languages { get; set; }

        [Required(ErrorMessage = "Please choose the genres")]
        public List<string>? Genres { get; set; }

        [Required(ErrorMessage = "Please choose the shelves")]
        public List<string>? Shelves { get; set; }
    }
}
