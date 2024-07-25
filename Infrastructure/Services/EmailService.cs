using DepartmentHierarchyAndReminderManagementSystem.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DepartmentHierarchyAndReminderManagementSystem.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpHost = _configuration["Email:Smtp:Host"];
                var smtpPort = int.Parse(_configuration["Email:Smtp:Port"]);
                var smtpUsername = _configuration["Email:Smtp:Username"];
                var smtpPassword = _configuration["Email:Smtp:Password"];
                var enableSsl = bool.Parse(_configuration["Email:Smtp:EnableSsl"]);
                var fromEmail = _configuration["Email:From"];

                _logger.LogInformation($"SMTP Configuration: Host={smtpHost}, Port={smtpPort}, Username={smtpUsername}, EnableSsl={enableSsl}");

                var smtpClient = new SmtpClient(smtpHost)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = enableSsl,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation($"Email sent to {toEmail}");
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, $"SMTP Exception: {smtpEx.Message}");
                if (smtpEx.InnerException is SocketException socketEx)
                {
                    _logger.LogError(socketEx, $"Socket Exception: {socketEx.Message}, ErrorCode: {socketEx.ErrorCode}");
                }
                throw new Exception($"SMTP Exception: {smtpEx.Message}", smtpEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception sending email: {ex.Message}");
                throw new Exception($"Exception sending email: {ex.Message}", ex);
            }
        }
    }
}
