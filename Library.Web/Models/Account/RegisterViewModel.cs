using Library.Model.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Library.Web.Models.Account
{
    public class RegisterViewModel
    {
        // To_Do: It will better to create resource file separately!

        [Display(Name = "Name:")]
        [Required(ErrorMessage = "Please enter first name!")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Please enter last name!")]
        public string? LastName { get; set; }

        [Display(Name = "DoB:")]
        [Required(ErrorMessage = "Please enter the Email Address!")]
        public DateTime? DOB { get; set; }

        [Display(Name = "Personal ID:")]
        public string? PersonalNumber { get; set; }

        [Display(Name = "Passport Number:")]
        public string? PassportNumber { get; set; }

        [Display(Name = "Phone Number:")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Address:")]
        public string? Address { get; set; }

        [Display(Name = "Gender:")]
        public Gender Gender { get; set; }

        [Display(Name = "Photo:")]
        public string? PersonalPhoto { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter the Email Address!")]
        [EmailAddress(ErrorMessage = "Email Address u entered is wrong!")]
        public string? Email { get; set; }

        [Display(Name = "Confirm Email")]
        [Required(ErrorMessage = "Please confirm the Email Address!")]
        [EmailAddress(ErrorMessage = "Email Address u entered not equals to email!")]
        public string? ConfirmEmail { get; set; }        

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords is not Equal!")]
        public string? ConfirmPassword { get; set; }
    }
}
