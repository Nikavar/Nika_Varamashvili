namespace Library.Web.Models.Email
{
    public class ResetPassword
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PasswordResetLink { get; set; }
    }
}
