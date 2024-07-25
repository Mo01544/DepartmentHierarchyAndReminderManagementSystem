using Microsoft.AspNetCore.Mvc;
using DepartmentHierarchyAndReminderManagementSystem.Application.Interfaces;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DepartmentHierarchyAndReminderManagementSystem.WebUI.Controllers
{
    public class ReminderController : Controller
    {
        private readonly IReminderService _service;

        public ReminderController(IReminderService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var reminders = await _service.GetAllReminders();
            return View(reminders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var reminder = await _service.GetReminderById(id);
            if (reminder == null)
            {
                return NotFound();
            }

            return View(reminder);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                await _service.AddReminder(reminder);
                return RedirectToAction(nameof(Index));
            }
            return View(reminder);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reminder = await _service.GetReminderById(id);
            if (reminder == null)
            {
                return NotFound();
            }

            return View(reminder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reminder reminder)
        {
            if (id != reminder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateReminder(reminder);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ReminderExists(reminder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reminder);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reminder = await _service.GetReminderById(id);
            if (reminder == null)
            {
                return NotFound();
            }

            return View(reminder);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteReminder(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ReminderExists(int id)
        {
            var reminder = await _service.GetReminderById(id);
            return reminder != null;
        }
    }
}
