using DepartmentHierarchyAndReminderManagementSystem.Application.Interfaces;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentHierarchyAndReminderManagementSystem.Infrastructure.Services
{
    public class ReminderEmailService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReminderEmailService> _logger;

        public ReminderEmailService(IServiceProvider serviceProvider, ILogger<ReminderEmailService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SendReminderEmailsAsync();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Check every minute
            }
        }

        private async Task SendReminderEmailsAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var reminderService = scope.ServiceProvider.GetRequiredService<IReminderService>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                var reminders = await reminderService.GetAllReminders();
                var upcomingReminders = reminders.Where(r => r.ReminderDateTime >= DateTime.Now && !r.EmailSent).ToList();

                try
                {
                    string toEmail = "ahmed.morsi10@hotmail.com";
                    string subject = "Test Email";
                    string body = "This is a test email to verify the email sending functionality.";

                    await emailService.SendEmailAsync(toEmail, subject, body);
                    _logger.LogInformation("Test email sent successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error sending test email.");
                }
            }
        }
    }
}
