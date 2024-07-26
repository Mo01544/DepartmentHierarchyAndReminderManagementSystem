using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DepartmentHierarchyAndReminderManagementSystem.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Logo { get; set; }

        public int? ParentDepartmentId { get; set; }

        [ValidateNever]
        public Department ParentDepartment { get; set; }

        [ValidateNever]
        public ICollection<Department> SubDepartments { get; set; }
    }
}
