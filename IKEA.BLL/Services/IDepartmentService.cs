﻿using IKEA.BLL.Models.Departments;
using IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentToReturnDTO> GetAllDepartments();
        DepartmentDetailsToReturnDTO? GetDepartmentById(int id);
        int CreatedDepartment(CreatedDepartmentDTO departmentDTO);
        int UpdateDepartment(UpdatedDepartmentDTO departmentDTO);
        bool DeleteDepartment(int id);
    }
}
