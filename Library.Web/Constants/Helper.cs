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


        #region EmailTemplateOperations
        public static async Task SendEmailTemplateAsync<TModel> (TModel model, IEmailService emailService, IConfiguration config, StaffReader? staffReader = null) where TModel : class
        {
            var emailTemplate = await emailService.GetManyEmailsAsync(x => x.TemplateType == model.GetType().Name);
            var template = emailTemplate.FirstOrDefault();

            string from = template.GetType().GetProperty("From").GetValue(template).ToString();
            string subject = template.GetType().GetProperty("Subject").GetValue(template).ToString();
            string body = template.GetType().GetProperty("Body").GetValue(template).ToString();

            string firstName, lastName, to;

            if(model is RegisterViewModel)
            {
                firstName = model.GetType().GetProperty("FirstName").GetValue(model).ToString();
                lastName = model.GetType().GetProperty("LastName").GetValue(model).ToString();
                to = model.GetType().GetProperty("Email").GetValue(model).ToString();
            }

            else
            {
                firstName = staffReader.FirstName;
                lastName = staffReader.LastName;
                to = staffReader.Email;
            }

            // Body Replacement

            if(!string.IsNullOrEmpty(body) && body != null)
            {
                body = body.Replace("{FirstName}", firstName);
                body = body.Replace("{LastName}", lastName);
            }

            // Mail Settings
            MailMessage mail = new MailMessage();

            // Check Logic!
            mail.From = new MailAddress(from);
            mail.Subject = subject;
            mail.Body = body;

            // var emailTemplate = await emailService.GetManyEmailsAsync(x => x.TemplateType == type);

            #region Smtp Settings

            SmtpClient smtp = new SmtpClient();

            smtp.Host = config.GetSection("MailSettings:Host").Value;
            bool hasPortNumber = int.TryParse(config.GetSection("MailSettings:Port").Value, out int port);
            smtp.Port = hasPortNumber == true ? port : 0;
            smtp.EnableSsl = true;
            string password = config.GetSection("MailSettings:Password").Value;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(from, password);

            #endregion

            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,                
            //    Credentials = new NetworkCredential(from, config.GetSection("MailSettings:Password").Value)
            //};

            // v.1
            await smtp.SendMailAsync(mail);
            
            // v.2
            await smtp.SendMailAsync(mail,CancellationToken.None);

            // v.3
            await smtp.SendMailAsync(from, to, subject, body);

        }
        #endregion


        #region TokenOperations
        // Generates token for Email Confirmation
        public static string TokenGeneration(string parameter, IConfiguration configuration)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, parameter),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTConfig:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var Token = new JwtSecurityToken(
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(10),
                 signingCredentials: credentials
             );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        // Generates token for User Login
        public static string TokenGeneration(User user, IConfiguration configuration)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.id.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfig:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Provides descryption for token key
        public static int TokenDecryption(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var jwtClaims = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            
            return jwtClaims != null ? int.Parse(jwtClaims.Value) : -1;
        }

        #endregion


    }
}

