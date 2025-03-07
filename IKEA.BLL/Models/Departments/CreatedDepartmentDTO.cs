﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Models.Departments
{
    public class CreatedDepartmentDTO
    {
        [Required(ErrorMessage = "Code is Required !!!")]
        public string Code { get; set; } = null!;

        [Required(ErrorMessage = "Name is Required !!!")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name ="Date Of Creation")]
        public DateOnly CreationDate { get; set; }

    }
}
