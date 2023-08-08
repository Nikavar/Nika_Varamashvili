namespace Library.Web.Models.Email
{
    public class EmailConfirmation
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ConfirmationLink { get; set; }
    }
}
