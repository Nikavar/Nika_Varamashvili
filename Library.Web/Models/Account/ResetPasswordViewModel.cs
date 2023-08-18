using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.Account
{
	public class ResetPasswordViewModel
	{
		public string? Token { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "New Password")]
		public string? Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		public string? ConfirmPassword { get; set; }
	}
}
