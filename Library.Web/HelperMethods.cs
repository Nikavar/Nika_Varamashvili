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


namespace Library.Web
{
    public static class HelperMethods
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

        public static async Task SendEmailTemplateAsync<TModel>(TModel model, IEmailService emailService, IConfiguration config, StaffReader? staffReader = null) where TModel : class
        {
            var emailTemplate = await emailService.GetManyEmailsAsync(x => x.TemplateType == model.GetType().Name);
            var template = emailTemplate.FirstOrDefault();

            string from = template.GetType().GetProperty("From").GetValue(template).ToString();
            string subject = template.GetType().GetProperty("Subject").GetValue(template).ToString();
            string body = template.GetType().GetProperty("Body").GetValue(template).ToString();

            //emailBody = emailBody.Replace("{#URL#}", url);

            string firstName, lastName, to;

            if (model is RegisterViewModel)
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

            if (!string.IsNullOrEmpty(body) && body != null)
            {
                body = body.Replace("{FirstName}", firstName);
                body = body.Replace("{LastName}", lastName);
            }

            //var smtpClient = new SmtpClient(config.GetSection("MailSettings:Host").Value, Convert.ToInt32(config.GetSection("MailSettings:Port").Value))
            //{
            //    EnableSsl = true,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(config.GetSection("MailSettings:Mail").Value, config.GetSection("MailSettings:Password").Value)
            //};

            //var mailMessage = new MailMessage
            //{
            //    From = new MailAddress(config.GetSection("MailSettings:Mail").Value),
            //    Subject = subject,
            //    Body = body,
            //    IsBodyHtml = true
            //};

            //mailMessage.To.Add(to);
            //mailMessage.To.Add(config.GetSection("MailSettings:BccMail").Value);
            //await smtpClient.SendMailAsync(mailMessage);


            #region Old Code for Mail Settings

            //// v.1
            bool hasPortNumber = int.TryParse(config.GetSection("MailSettings:Port").Value, out int port);
            var _port = hasPortNumber == true ? port : 0;
            var _host = config.GetSection("MailSettings:Host").Value;

            var username = config.GetSection("MailSettings:Username").Value;
			var password = config.GetSection("MailSettings:Password").Value;


            SmtpClient smtpClient = new SmtpClient(_host, _port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password)
            };

            // Mail Settings
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(to);
			mailMessage.To.Add(config.GetSection("MailSettings:BccMail").Value);
			await smtpClient.SendMailAsync(mailMessage);
			
			#endregion
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

                PropertyInfo idProperty = entity.GetType().GetProperty("id") ?? entity.GetType().GetProperty("ID")
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

