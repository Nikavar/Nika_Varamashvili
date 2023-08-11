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
using Library.Web.Models.Account;
using System.Security.Cryptography;
using Library.Web.Enums;
using Windows.UI.Xaml;

namespace Library.Web.HelperMethods
{
    public static class EmailHelper
    {
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static async Task SendEmailAsync<TModel>(TModel model, IEmailService emailService, IConfiguration config, string emailTo) where TModel : class
        {

            Type modelType = model.GetType();

            var _templateData = await emailService.GetManyEmailsAsync(x => x.TemplateType.ToLower() == modelType.Name.ToLower());
            var body = _templateData?.FirstOrDefault()?.Body;
            var subject = _templateData?.FirstOrDefault()?.Subject;

            PropertyInfo[] properties = modelType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string placeholder = "{" + property.Name + "}";
                string value = property?.GetValue(model)?.ToString();
                body = body?.Replace(placeholder, value);
            }

            if (subject != null && body != null)
                await SendEmailTemplateAsync(emailTo, subject, config, body);
        }

        private static async Task SendEmailTemplateAsync(string email, string subject, IConfiguration config, string body)
        {
            var smtpClient = new SmtpClient(config.GetSection("MailSettings:Host").Value, Convert.ToInt32(config.GetSection("MailSettings:Port").Value))
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(config.GetSection("MailSettings:Username").Value, config.GetSection("MailSettings:Password").Value)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(config.GetSection("MailSettings:From").Value),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);
            mailMessage.To.Add(config.GetSection("MailSettings:BccMail").Value);
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}

