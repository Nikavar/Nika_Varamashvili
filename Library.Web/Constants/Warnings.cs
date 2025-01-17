﻿using Library.Model.Models;
using System.Drawing;

namespace Library.Web.Constants
{
	public static class Warnings
	{
		public const string PasswordAlreadyExists = "the same password already exists!";
		public const string EmailAlreadyExists = "the same email already exists!";
		public const string UserIsAlreadyRegistered = "This user is already exists!";
		public const string SuccessfullyAdded = "Successfully added!";
		public const string UsernameOrPasswordIsIncorrect = "username or password is incorrect!";
		public const string ConfirmEmailSubject = "Please Confirm your email!";
		public const string EmailWasConfirmed = "Your Email was Confirmed!";
		public const string ConfirmationEmailBody = "Dear, \n{FirstName} {LastName}, \nPlease Confirm your email address <a href=\"{#URL#}\"> CLICK here </a>";
		public const string UserNotExistsOrNotConfirmed = "User does not exist or is not confirmed";
		public const string SuccesfullyChanged = "Successfully Changed";
        public const string SomethingWasWrong = "Something was Wrong";
        public const string ConfirmYourEmail = "please confirm your email by clicking on the confirmation link we have emailed you";
        public const string PasswordsMatch = "Passwords are the same";

        #region GenericWarnings
        public static string SuccessfullyAddedGeneric<T>()
        {
            return "New " + typeof(T).Name + " has Successfully Added";
        }
        public static string SuccessfullyUpdatedGeneric<T>()
        {
            return "The " + typeof(T).Name + " has Successfully Updated";
        }
        public static string SuccessfullyDeletedGeneric<T>()
        {
            return "The " + typeof(T).Name + " has Successfully Deleted";
        }

        #endregion
    }
}
