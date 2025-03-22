using IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Models.Employees
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        [DataType(DataType.Currency)]
        [RegularExpression(@"^[0-9]{1,3}[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }
        public decimal? Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [Display(Name ="Phone Number")]
        public string? PhoneNumber { get; set; }
        [Display(Name ="Hirring date")]
        public DateTime HirringDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModificationBy { get; set; }
        public DateTime LastModificationOn { get; set; }
        public string? Department { get; set; }
        public string? Image {  get; set; }

    }
}
