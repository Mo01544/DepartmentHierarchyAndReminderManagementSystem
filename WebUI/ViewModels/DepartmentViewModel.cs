using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using System.Collections.Generic;

namespace DepartmentHierarchyAndReminderManagementSystem.WebUI.ViewModels
{
    public class DepartmentViewModel
    {
        public Department Department { get; set; }
        public IEnumerable<Department> ParentDepartments { get; set; }
    }
}
