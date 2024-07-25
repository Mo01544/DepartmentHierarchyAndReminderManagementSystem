using Microsoft.EntityFrameworkCore;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Interfaces;
using DepartmentHierarchyAndReminderManagementSystem.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentHierarchyAndReminderManagementSystem.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Department> GetDepartmentWithSubDepartments(int id)
        {
            return await _context.Departments
                .Include(d => d.SubDepartments)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Department>> GetParentDepartments(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            var parents = new List<Department>();

            while (department?.ParentDepartmentId != null)
            {
                department = await _context.Departments.FindAsync(department.ParentDepartmentId);
                parents.Add(department);
            }

            return parents;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task AddDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
