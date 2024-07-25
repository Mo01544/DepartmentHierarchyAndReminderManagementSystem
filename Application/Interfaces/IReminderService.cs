using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepartmentHierarchyAndReminderManagementSystem.Application.Interfaces
{
    public interface IReminderService
    {
        Task<IEnumerable<Reminder>> GetAllReminders();
        Task<Reminder> GetReminderById(int id);
        Task AddReminder(Reminder reminder);
        Task UpdateReminder(Reminder reminder);
        Task DeleteReminder(int id);
    }
}
