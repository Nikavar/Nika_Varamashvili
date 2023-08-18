using Library.Model.Models;

namespace Library.Web.Models.Book
{
    public class CreateBookViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<Author>? Authors { get; set; }
        public List<Publisher>? Publishers { get; set; }
        public List<Language>? Languages { get; set; }
        public List<Category>? Genres { get; set; }
        public List<Storage>? Shelves { get; set; }
    }
}
