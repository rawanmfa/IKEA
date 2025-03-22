using IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Models.Employees
{
    public class UpdatedEmployeeDto
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Max lenght of Name is 50 char")]
        [MinLength(3, ErrorMessage = "Min lenght of Name is 3 char")]
        public string Name { get; set; } = null!;
        [Range(22, 30)]
        public int? Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address must be like 123-Street-City")]
        public string? Address { get; set; }
        public decimal? Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hirring date")]
        public DateTime HirringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentId { get; set; }
        public string? Image {  get; set; }
    }
}
