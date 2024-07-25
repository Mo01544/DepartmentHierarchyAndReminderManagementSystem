﻿using System.Threading.Tasks;

namespace DepartmentHierarchyAndReminderManagementSystem.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
