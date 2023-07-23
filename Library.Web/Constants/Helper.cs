using Humanizer.Localisation;
using Library.Model.Models;
using Library.Service;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml.Vml.Office;
using System.Security.Cryptography.Pkcs;
using System.Configuration;
using Windows.ApplicationModel.Email;
using Library.Web.Models.Account;
using System.Drawing;

namespace Library.Web.Constants
{
    public static class Helper
    {
        public static async Task EmailLinkConfirmation(string EmailTo, string url, StaffReader staffReader, IConfiguration configuration)
        {
            var emailBody = Warnings.ConfirmationEmailBody;

            emailBody = emailBody.Replace("{FirstName}", staffReader.FirstName);
            emailBody = emailBody.Replace("{LastName}", staffReader.LastName);
            emailBody = emailBody.Replace("{#URL#}", url);
            await SendEmailAsync(EmailTo, Warnings.ConfirmEmailSubject, emailBody, configuration);
        }


        // Send Email for confirmation a link
        public static async Task SendEmailAsync(string email, string subject, string message, IConfiguration configuration)
        {
            var smtpClient = new SmtpClient(configuration.GetSection("MailSettings:Host").Value, configuration.GetValue<int>("MailSettings:Port"))
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(configuration.GetSection("MailSettings:From").Value, configuration.GetSection("MailSettings:Password").Value)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(configuration.GetSection("MailSettings:From").Value),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);
            mailMessage.To.Add(configuration.GetSection("MailSettings:Bccmail").Value);
            await smtpClient.SendMailAsync(mailMessage);
        }

        // Forget & Reset Password

        public static void AppSettings(out string ToMailText, out string Password, out string SMTPPort, out string Host, IConfiguration configuration)
		{
			ToMailText = configuration.GetSection("MailSettings:ToMailText").Value;
			Password = configuration.GetSection("MailSettings:Password").Value;
			SMTPPort = configuration.GetSection("MailSettings:Port").Value;
			Host = configuration.GetSection("MailSettings:Host").Value;
		}
		public static void SendEmail(string From, string Subject, string Body, string To, string ToMailText, string Password, string SMTPPort, string Host)
		{
			System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
			mail.To.Add(To);
			mail.From = new MailAddress(From);
			mail.Subject = Subject;
			mail.Body = Body;
            mail.IsBodyHtml = true;

			SmtpClient smtp = new SmtpClient();
			smtp.Host = Host;
			smtp.Port = Convert.ToInt16(SMTPPort);
			smtp.Credentials = new NetworkCredential(From, Password);
			smtp.EnableSsl = true;
			smtp.Send(mail);
		}     
    }
}

