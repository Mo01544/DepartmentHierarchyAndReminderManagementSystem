using DepartmentHierarchyAndReminderManagementSystem.Domain.Entities;
using System.Collections.Generic;

namespace DepartmentHierarchyAndReminderManagementSystem.WebUI.ViewModels
{
    public class DepartmentDetailsViewModel
    {
        public Department Department { get; set; }
        public List<Department> ParentDepartments { get; set; }
        public List<Department> SubDepartments { get; set; }
    }
}
