using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Interfaces;
using DepartmentHierarchyAndReminderManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.Include(d => d.SubDepartments).ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var department = await _context.Departments.Include(d => d.SubDepartments).FirstOrDefaultAsync(d => d.Id == id);
            if (department != null && department.SubDepartments == null)
            {
                department.SubDepartments = new List<Department>();
            }
            return department;
        }

        public async Task AddDepartmentAsync(Department department)
        {
            if (department.SubDepartments == null)
            {
                department.SubDepartments = new List<Department>();
            }
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            if (department.SubDepartments == null)
            {
                department.SubDepartments = new List<Department>();
            }
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
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
