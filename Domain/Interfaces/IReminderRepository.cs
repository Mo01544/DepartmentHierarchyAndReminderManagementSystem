using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepartmentHierarchyAndReminderManagementSystem.Domain.Interfaces
{
    public interface IReminderRepository
    {
        Task<IEnumerable<Reminder>> GetAllReminders();
        Task<Reminder> GetReminderById(int id);
        Task AddReminder(Reminder reminder);
        Task UpdateReminder(Reminder reminder);
        Task DeleteReminder(int id);
    }
}
