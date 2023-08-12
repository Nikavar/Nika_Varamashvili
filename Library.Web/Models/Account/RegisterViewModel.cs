using Library.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.Account
{
	public class RegisterViewModel
	{
		// To_Do: It will better to create resource file separately!
		public int Id { get; set; }
		public int PositionId { get; set; }
		public List<SelectListItem>? Positions { get; set; }

		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public string? Position { get; set; }

		public DateTime? DOB { get; set; }

		public string? PersonalNumber { get; set; }

		public string? PassportNumber { get; set; }

		public string? PhoneNumber { get; set; }

		public string? Address { get; set; }

		[Display(Name = "Gender:")]
		public bool Gender { get; set; } = true;

		[Display(Name = "Photo:")]
		public string? PersonalPhoto { get; set; }

		public string? Email { get; set; }

		//[Display(Name = "Confirm Email")]
		//[Required(ErrorMessage = "Please confirm the Email Address!")]
		//[EmailAddress(ErrorMessage = "Email Address u entered not equals to email!")]
		public string? ConfirmEmail { get; set; }

		[DataType(DataType.Password)]
		public string? Password { get; set; }

		[DataType(DataType.Password)]
		public string? ConfirmPassword { get; set; }
	}
}
