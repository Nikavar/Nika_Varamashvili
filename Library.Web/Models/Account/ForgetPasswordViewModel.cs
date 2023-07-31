using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.Account
{
	public class ForgetPasswordViewModel
	{
		[Required]
		[Display(Name = "Email")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string? Email { get; set; }

		public string? ForgetURL { get; set; }
	}
}
