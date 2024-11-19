using api;
using Microsoft.Extensions.Options;
using Project.Api.Helpers;
using Project.Core.Interfaces.IServices.IOtherServices;
using System.Net;
using System.Net.Mail;

namespace Project.Core.Services.OtherServices
{
    public class MailService : IMailService
    {
        private readonly SmtpClient _smtp;
        IOptions<EmailConfig> _emailConfig;
        public MailService(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig;
            _smtp = new SmtpClient(emailConfig.Value.SmtpClient)
            {
                Port = _emailConfig.Value.Port,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailConfig.Value.EmailSender, _emailConfig.Value.Key),
                EnableSsl = true
            };
        }
        public Task SendBookingConfirmation(string addressee)
        {
            throw new NotImplementedException();
        }

        public async Task SendConfirmToken(string addressee, string token, string userId, string requestUrl)
        {
            string emailTemplate = LoadEmailTemplate("Templates/accountConfirmationTemplate.html");
            string confirmationLink = $"{requestUrl}{_emailConfig.Value.EmailConfirmReturnPath}?token={token}&userId={userId}";
            string emailBody = emailTemplate.Replace("{{ConfirmationLink}}", confirmationLink);
            await SendEmail(addressee, "Rafting - Potwierdź konto", emailBody);
        }

        public Task SendPasswordResetToken(string addressee, string token, string requestUrl)
        {
            throw new NotImplementedException();
        }

        private string LoadEmailTemplate(string templatePath)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), templatePath);
            if (!File.Exists(filePath))
                throw new Exception($"Email template not found: {filePath}");

            return File.ReadAllText(filePath);
        }

        private async Task SendEmail(string addressee, string subject, string body)
        {
            var mail = new MailMessage(_emailConfig.Value.EmailSender, addressee)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            await _smtp.SendMailAsync(mail);
        }

    }
}