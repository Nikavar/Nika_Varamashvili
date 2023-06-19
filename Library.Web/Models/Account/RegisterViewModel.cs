using Library.Model.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.Account
{
	public class RegisterViewModel
	{
		// To_Do: It will better to create resource file separately!

		[Display(Name = "First Name:")]
		[Required(ErrorMessage = "Please enter first name!")]
		public string? FirstName { get; set; }

		[Display(Name = "Last Name:")]
		[Required(ErrorMessage = "Please enter last name!")]
		public string? LastName { get; set; }

		[Display(Name = "Positions")]
		public string? Position { get; set; }

		[Display(Name = "DoB:")]
		[Required(ErrorMessage = "Please enter day of birth!")]
		public DateTime? DOB { get; set; }

		[Display(Name = "Personal ID:")]
		public string? PersonalNumber { get; set; }

		[Display(Name = "Passport Number:")]
		public string? PassportNumber { get; set; }

		[Required]
		[Display(Name = "Phone Number:")]
		public string? PhoneNumber { get; set; }

		[Display(Name = "Address:")]
		public string? Address { get; set; }

		[Display(Name = "Gender:")]
		public bool Gender { get; set; } = true;

		[Display(Name = "Photo:")]
		public string? PersonalPhoto { get; set; }


		[Display(Name = "Email")]
		[Required(ErrorMessage = "Please enter the Email Address!")]
		[EmailAddress(ErrorMessage = "Email Address u entered is wrong!")]
		[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
			ErrorMessage = "Passwords must have at least 8 characters and contain at least 3 of 4 the following: uppercase letters, lowercase letters, numbers (0-9), and symbols.")]

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
