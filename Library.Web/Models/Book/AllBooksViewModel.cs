using Library.Model.Models;

namespace Library.Web.Models.Book
{
	public class AllBooksViewModel
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public IFormFile? imgfile { get; set; }
		public List<Author>? Authors { get; set; }
		public List<Publisher>? Publishers { get; set; }
		public List<Category>? Genres { get; set; }
		public List<Storage>? Shelves { get; set; }

	}
}
