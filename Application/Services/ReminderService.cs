using DepartmentHierarchyAndReminderManagementSystem.Application.Interfaces;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepartmentHierarchyAndReminderManagementSystem.Application.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _repository;

        public ReminderService(IReminderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Reminder>> GetAllReminders()
        {
            return await _repository.GetAllReminders();
        }

        public async Task<Reminder> GetReminderById(int id)
        {
            return await _repository.GetReminderById(id);
        }

        public async Task AddReminder(Reminder reminder)
        {
            await _repository.AddReminder(reminder);
        }

        public async Task UpdateReminder(Reminder reminder)
        {
            await _repository.UpdateReminder(reminder);
        }

        public async Task DeleteReminder(int id)
        {
            await _repository.DeleteReminder(id);
        }
    }
}
