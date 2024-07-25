using Microsoft.AspNetCore.Mvc;
using DepartmentHierarchyAndReminderManagementSystem.Application.Interfaces;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using DepartmentHierarchyAndReminderManagementSystem.WebUI.ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DepartmentHierarchyAndReminderManagementSystem.WebUI.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _service.GetAllDepartments();
            return View(departments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var department = await _service.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _service.AddDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var department = await _service.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateDepartment(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DepartmentExists(department.Id))
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
            return View(department);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var department = await _service.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteDepartment(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DepartmentExists(int id)
        {
            var department = await _service.GetDepartmentById(id);
            return department != null;
        }
    }
}
