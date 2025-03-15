using IKEA.DAL.Common.Enums;
using IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Models.Employees
{
    public class Employee:ModelBase
    {
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal? Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set;}
        public string? PhoneNumber { get; set; }
        public DateTime HirringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        #region Navigational prperty
        #region Work
        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }
        [InverseProperty(nameof(Models.Departments.Department.Employees))]
        public Department? Department { get; set; }
        #endregion
        #region Manage
        [InverseProperty(nameof(Models.Departments.Department.Manager))]
        public Department? ManageDepartment { get; set; }
        #endregion
        #endregion
    }
}
