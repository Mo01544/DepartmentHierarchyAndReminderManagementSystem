using DepartmentHierarchyAndReminderManagementSystem.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DepartmentHierarchyAndReminderManagementSystem.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient(_configuration["Email:Smtp:Host"])
            {
                Port = int.Parse(_configuration["Email:Smtp:Port"]),
                Credentials = new NetworkCredential(_configuration["Email:Smtp:Username"], _configuration["Email:Smtp:Password"]),
                EnableSsl = bool.Parse(_configuration["Email:Smtp:EnableSsl"]),
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:From"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
