using IKEA.DAL.Models.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Models.Departments
{
    public class Department:ModelBase
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }
        #region Work
        [InverseProperty(nameof(Models.Employees.Employee.Department))]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        #endregion
        #region Manage
        [ForeignKey(nameof(Manager))]
        public int? ManagerId { get; set; }
        [InverseProperty(nameof(Models.Employees.Employee.ManageDepartment))]
        public Employee? Manager { get; set; }
        #endregion
    }
}
