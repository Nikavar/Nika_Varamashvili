﻿using Library.Model.Models;
using Microsoft.AspNetCore.Mvc;
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

		[Required]
		[RegularExpression("^[a-zA-Z ]*$",ErrorMessage = "Input Format is only text")]
		public string? FirstName { get; set; }

		[Required]
		[RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Input Format is only text")]
		public string? LastName { get; set; }

		public string? Position { get; set; }

		[Required]
		public DateTime? DOB { get; set; }

		[MaxLength(11, ErrorMessage = "personal id must be 11 digits")]
		public string? PersonalNumber { get; set; }

		public string? PassportNumber { get; set; }

		[Required]
		public string? PhoneNumber { get; set; }

		[Required]
		public string? Address { get; set; }

		[Display(Name = "Gender:")]
		public bool Gender { get; set; } = true;

		[Display(Name = "Photo:")]
		public string? PersonalPhoto { get; set; }

		[Required]
		[RegularExpression("[a-z0-9]+@[a-z]+\\.[a-z]{2,3}",ErrorMessage = "Input value in Mail Format")]
		[DataType(DataType.EmailAddress)]
		[EmailAddress(ErrorMessage = "Email format is not correct!")]
		public string? Email { get; set; }		

		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage = "Passwords don't match")]
		public string? ConfirmPassword { get; set; }
	}
}
