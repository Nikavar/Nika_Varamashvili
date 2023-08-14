using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.Account
{
	public class UserLoginViewModel
	{
		public string? EmailAddress { get; set; }

		[DataType(DataType.Password)]
		public string? Password { get; set; }
	}
}
