using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Library.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter the Email Address!")]
        [EmailAddress(ErrorMessage = "Email Address u entered is wrong!")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords is not Equal!")]
        public string ConfirmPassword { get; set; }
    }
}
