﻿using IKEA.BLL.Models.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _DepartmentRepository;

        public DepartmentService(IDepartmentRepository DepartmentRepository)
        {
            _DepartmentRepository = DepartmentRepository;
        }

        public IEnumerable<DepartmentToReturnDTO> GetAllDepartments() // this function different from what the instructor did if error occured or the retured data look different look MVC session4 second video at 0:30
        {
            var departments = _DepartmentRepository.GetAll();
            foreach (var department in departments)
            {
                yield return new DepartmentToReturnDTO
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    CreationDate = department.CreationDate,
                };
            }
        }
       
        public DepartmentDetailsToReturnDTO? GetDepartmentById(int id)
        {
            var department = _DepartmentRepository.GetById(id);
            if (department is not null /* you can write not null like that { }*/)
            {
                return new DepartmentDetailsToReturnDTO
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModificationBy = department.LastModificationBy,
                    LastModificationOn = department.LastModificationOn,
                };
            }
            return null;
        }

        public int CreatedDepartment(CreatedDepartmentDTO departmentDTO)
        {
            var CreatedDapartment = new Department()
            {
                Code = departmentDTO.Code,
                Name = departmentDTO.Name,
                Description = departmentDTO.Description,
                CreationDate = departmentDTO.CreationDate,
                CreatedBy = 1,
                LastModificationBy= 1,
                LastModificationOn = DateTime.UtcNow,
                // CreatedOn= DateTime.UtcNow, this will be done throught migration cause i changed it in the configuration file
            };
            return _DepartmentRepository.Add(CreatedDapartment);
        }

        public int UpdateDepartment(UpdatedDepartmentDTO departmentDTO)
        {
            var updatedDapartment = new Department()
            {
                Code = departmentDTO.Code,
                Name = departmentDTO.Name,
                Description = departmentDTO.Description,
                CreationDate = departmentDTO.CreationDate,
                CreatedBy = 1,
                LastModificationBy = 1,
                LastModificationOn = DateTime.UtcNow,
            };
            return _DepartmentRepository.Update(updatedDapartment);

        }
        public bool DeleteDepartment(int id)
        {
            var department = _DepartmentRepository.GetById(id);
            if (department is not null)
            {
                return _DepartmentRepository.Delete(department) > 0;
            }
            return false;
        }

    }
}
