using Library.Model.Models;
using Library.Service;
using Library.Web.Models.Account;
using System.Net.Mail;
using System.Net;

namespace Library.Web.Constants
{
    public static class EmailMethods
    {
        public static async Task SendEmailTemplateAsync<TModel>(TModel model, IEmailService emailService, IConfiguration config, StaffReader? staffReader = null) where TModel : class
        {
            var emailTemplate = await emailService.GetManyEmailsAsync(x => x.TemplateType == model.GetType().Name);
            var template = emailTemplate.FirstOrDefault();

            string from = template.GetType().GetProperty("From").GetValue(template).ToString();
            string subject = template.GetType().GetProperty("Subject").GetValue(template).ToString();
            string body = template.GetType().GetProperty("Body").GetValue(template).ToString();

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

            // Mail Settings
            MailMessage mail = new MailMessage();

            // Check Logic!
            mail.From = new MailAddress(from);
            mail.Subject = subject;
            mail.Body = body;

            // var emailTemplate = await emailService.GetManyEmailsAsync(x => x.TemplateType == type);

            #region Smtp Settings

            // v.1
            SmtpClient smtp = new SmtpClient();

            smtp.Host = config.GetSection("MailSettings:Host").Value;
            bool hasPortNumber = int.TryParse(config.GetSection("MailSettings:Port").Value, out int port);
            smtp.Port = hasPortNumber == true ? port : 0;
            smtp.EnableSsl = true;
            string password = config.GetSection("MailSettings:Password").Value;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(from, password);

            // v.2

            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,                
            //    Credentials = new NetworkCredential(from, config.GetSection("MailSettings:Password").Value)
            //};
            #endregion


            // v.1
            await smtp.SendMailAsync(mail);

            // v.2
            await smtp.SendMailAsync(mail, CancellationToken.None);

            // v.3
            await smtp.SendMailAsync(from, to, subject, body);

        }
    }
}
