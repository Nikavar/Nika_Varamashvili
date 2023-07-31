using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.Account
{
	public class UserLoginViewModel
	{
		[Display(Name = "Email")]
		[Required(ErrorMessage = "You have to enter your Email Address!")]
		[EmailAddress(ErrorMessage = "Invalid Email Address!")]
		public string EmailAddress { get; set; }

		[Required(ErrorMessage = "You have to enter your Password!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
