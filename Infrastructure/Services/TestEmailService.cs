using DepartmentHierarchyAndReminderManagementSystem.Application.Interfaces;
using Microsoft.Extensions.Logging;

public class TestEmailService
{
    private readonly IEmailService _emailService;
    private readonly ILogger<TestEmailService> _logger;

    public TestEmailService(IEmailService emailService, ILogger<TestEmailService> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task TestSendEmail()
    {
        try
        {
            string toEmail = "ahmed.morsi10@hotmail.com";
            string subject = "Test Email";
            string body = "This is a test email to verify the email sending functionality.";

            await _emailService.SendEmailAsync(toEmail, subject, body);
            _logger.LogInformation("Test email sent successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending test email.");
        }
    }
}
