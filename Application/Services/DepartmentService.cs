using DepartmentHierarchyAndReminderManagementSystem.Application.Interfaces;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepartmentHierarchyAndReminderManagementSystem.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            return await _repository.GetAllDepartmentsAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await _repository.GetDepartmentByIdAsync(id);
        }

        public async Task AddDepartment(Department department)
        {
            await _repository.AddDepartmentAsync(department);
        }

        public async Task UpdateDepartment(Department department)
        {
            await _repository.UpdateDepartmentAsync(department);
        }

        public async Task DeleteDepartment(int id)
        {
            await _repository.DeleteDepartmentAsync(id);
        }
    }
}
