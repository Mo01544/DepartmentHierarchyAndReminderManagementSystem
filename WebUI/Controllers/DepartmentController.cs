using Microsoft.AspNetCore.Mvc;
using DepartmentHierarchyAndReminderManagementSystem.Application.Interfaces;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentHierarchyAndReminderManagementSystem.WebUI.ViewModels;
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

            var parentDepartments = await GetParentDepartments(department);
            var subDepartments = await GetSubDepartments(department);

            var model = new DepartmentDetailsViewModel
            {
                Department = department,
                ParentDepartments = parentDepartments,
                SubDepartments = subDepartments
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _service.GetAllDepartments();
            return View(new Department());
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
            ViewBag.Departments = await _service.GetAllDepartments();
            return View(department);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var department = await _service.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }

            ViewBag.Departments = await _service.GetAllDepartments();
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
            ViewBag.Departments = await _service.GetAllDepartments();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteDepartment(id);
                return Json(new { success = true, message = "Department deleted successfully." });
            }
            catch
            {
                return Json(new { success = false, message = "Error deleting department." });
            }
        }

        private async Task<bool> DepartmentExists(int id)
        {
            var department = await _service.GetDepartmentById(id);
            return department != null;
        }

        private async Task<List<Department>> GetParentDepartments(Department department)
        {
            var parents = new List<Department>();
            var currentDepartment = department;
            while (currentDepartment.ParentDepartmentId != null)
            {
                currentDepartment = await _service.GetDepartmentById((int)currentDepartment.ParentDepartmentId);
                if (currentDepartment != null)
                {
                    parents.Add(currentDepartment);
                }
            }
            return parents;
        }

        private async Task<List<Department>> GetSubDepartments(Department department)
        {
            var subDepartments = new List<Department>();
            if (department.SubDepartments != null)
            {
                foreach (var subDepartment in department.SubDepartments)
                {
                    subDepartments.Add(subDepartment);
                    subDepartments.AddRange(await GetSubDepartments(subDepartment));
                }
            }
            return subDepartments;
        }
    }
}
