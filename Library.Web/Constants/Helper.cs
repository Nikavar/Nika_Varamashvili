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
        public static int pageSize = 10;

        #region GenericWarnings
        public static string SuccessfullyAdded<T>()
        {
            return "New " + typeof(T).Name + " has Successfully Added";
        }
        public static string SuccessfullyUpdated<T>()
        {
            return "The " + typeof(T).Name + " has Successfully Updated";
        }
        public static string SuccessfullyDeleted<T>()
        {
            return "The " + typeof(T).Name + " has Successfully Deleted";
        }

        #endregion


        #region EntityOperationsWithLogs
        public static async Task AddEntityWithLog<TEntity>(TEntity entity, Func<TEntity, Task<TEntity>> AddEntityAsync, ILogService logService) where TEntity : class
        {
            var log = new LogInfo { TableName = typeof(TEntity).Name };

            try
            {
                entity = await AddEntityAsync(entity);
                var entityJson = JsonConvert.SerializeObject(entity, Formatting.Indented);
                log.LogContent = entityJson;
                log.LogStatus = LogStatus.Info.ToString();

                PropertyInfo idProperty =   entity.GetType().GetProperty("id") ?? entity.GetType().GetProperty("ID")
                                            ?? entity.GetType().GetProperty("Id");

                if (idProperty != null)
                {
                    log.EntityID = (int)idProperty.GetValue(entity);
                }
            }
            catch (Exception ex)
            {
                log.LogContent = ex.Message;
                log.LogStatus = LogStatus.Error.ToString();
            }
            finally
            {
                await logService.AddLogAsync(log);
            }
        }

        public static async Task UpdateEntityWithLog<TEntity>(TEntity entity, Func<TEntity, Task> UpdateEntityAsync, ILogService logService) where TEntity : class
        {
            PropertyInfo idProperty = entity.GetType().GetProperty("id") ?? entity.GetType().GetProperty("ID")
                            ?? entity.GetType().GetProperty("Id");

            LogInfo log = new LogInfo();

            if (idProperty != null)
            {
                log = await logService.GetLogByEntityId((int)idProperty.GetValue(entity));
            }

            try
            {
                await UpdateEntityAsync(entity);
                var entityJson = JsonConvert.SerializeObject(entity, Formatting.Indented);
                log.LogContent = entityJson;
                log.LogStatus = LogStatus.Info.ToString();
            }

            catch (Exception ex)
            {
                log.LogContent = ex.Message;
                log.LogStatus = LogStatus.Error.ToString();
            }

            finally
            {
                await logService.UpdateLogAsync(log);
            }
        }

        #endregion

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
        public static async Task SendEmailTemplateAsync(Email template, IEmailService emailService, IConfiguration config)
        {
            //string From = template.GetType().GetProperty("From").GetValue(template).ToString();
            //string To = template.GetType().GetProperty("To").GetValue(template).ToString();
            //string Subject = template.GetType().GetProperty("Subject").GetValue(template).ToString();
            //string Body = template.GetType().GetProperty("Body").GetValue(template).ToString();

            string From = template.From;
            //string To = template.To;
            string Subject = template.Subject;
            string Body = template.Body;

            // Mail Settings
            MailMessage mail = new MailMessage();

            // Check Logic!

            if (From != null)
                mail.From = new MailAddress(From);

                mail.Subject = Subject;
                mail.Body = Body;

           // var emailTemplate = await emailService.GetManyEmailsAsync(x => x.TemplateType == type);


            // Smtp Settings

            SmtpClient smtp = new SmtpClient();
            smtp.EnableSsl = true;
            smtp.Host = config.GetSection("MailSettings:Host").Value;
            bool hasPortNumber = int.TryParse(config.GetSection("MailSettings:Port").Value, out int port);
            smtp.Port = hasPortNumber == true? port : 0;
                
            string Password = config.GetSection("MailSettings:Password").Value;
            smtp.Credentials = new NetworkCredential(From, Password);
            await smtp.SendMailAsync(mail);            

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

