using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepartmentHierarchyAndReminderManagementSystem.Domain.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department> GetDepartmentWithSubDepartments(int id);
        Task<IEnumerable<Department>> GetParentDepartments(int id);
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department> GetDepartmentById(int id);
        Task AddDepartment(Department department);
        Task UpdateDepartment(Department department);
        Task DeleteDepartment(int id);
    }
}
