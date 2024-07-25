using DepartmentHierarchyAndReminderManagementSystem.Application.Interfaces;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Interfaces;
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

        public async Task<Department> GetDepartmentWithSubDepartments(int id)
        {
            return await _repository.GetDepartmentWithSubDepartments(id);
        }

        public async Task<IEnumerable<Department>> GetParentDepartments(int id)
        {
            return await _repository.GetParentDepartments(id);
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _repository.GetAllDepartments();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await _repository.GetDepartmentById(id);
        }

        public async Task AddDepartment(Department department)
        {
            await _repository.AddDepartment(department);
        }

        public async Task UpdateDepartment(Department department)
        {
            await _repository.UpdateDepartment(department);
        }

        public async Task DeleteDepartment(int id)
        {
            await _repository.DeleteDepartment(id);
        }
    }
}
