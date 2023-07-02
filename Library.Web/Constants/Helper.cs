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

namespace Library.Web.Constants
{
    public static class Helper
    {
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

        public static async Task EmailLinkConfirmation(string EmailTo, string url, IConfiguration configuration, StaffReader staffReader)
        {
            var emailBody = Warnings.ConfirmationEmailBody;

            emailBody = emailBody.Replace("{FirstName}", staffReader.FirstName);
            emailBody = emailBody.Replace("{LastName}", staffReader.LastName);
            emailBody = emailBody.Replace("{#URL#}", url);
            await SendEmailAsync(EmailTo, Warnings.ConfirmEmailSubject, emailBody, configuration);
        }

        public static async Task SendEmailAsync(string email, string subject, string message, IConfiguration configuration)
        {
            var smtpClient = new SmtpClient(configuration.GetSection("MailSettings:Host").Value, configuration.GetValue<int>("MailSettings:Port"))
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
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

        // Generates token for Email Confirmation
        public static string TokenGeneration(string id, IConfiguration configuration)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id),
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
            var jwtData = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            
            return jwtData != null ? int.Parse(jwtData.Value) : -1;
        }
    }
}
