using Microsoft.EntityFrameworkCore;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Interfaces;
using DepartmentHierarchyAndReminderManagementSystem.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentHierarchyAndReminderManagementSystem.Infrastructure.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly ApplicationDbContext _context;

        public ReminderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reminder>> GetAllReminders()
        {
            return await _context.Reminders.ToListAsync();
        }

        public async Task<Reminder> GetReminderById(int id)
        {
            return await _context.Reminders.FindAsync(id);
        }

        public async Task AddReminder(Reminder reminder)
        {
            await _context.Reminders.AddAsync(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReminder(Reminder reminder)
        {
            _context.Reminders.Update(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReminder(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder != null)
            {
                _context.Reminders.Remove(reminder);
                await _context.SaveChangesAsync();
            }
        }
    }
}
