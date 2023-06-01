using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.Account
{
	public class UserLoginViewModel
	{
		[Key]
		public int UserId { get; set; }

		[Display(Name = "Email")]
		[Required(ErrorMessage = "U have to enter Your Email!")]
		[EmailAddress(ErrorMessage = "Invalid Email Address!")]
		public string EmailAddress { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
