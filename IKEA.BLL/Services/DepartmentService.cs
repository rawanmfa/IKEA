using IKEA.BLL.Models.Departments;
using IKEA.BLL.Models.Employees;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Repositories.Departments;
using IKEA.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DepartmentToReturnDTO>> GetAllDepartmentsAsync()
        {
            return await _unitOfWork.DepartmentRepository.GetAllAsQuerable()
                .Where(e => !e.IsDeleted)
                .Select(department => new DepartmentToReturnDTO()
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    CreationDate = department.CreationDate,
                }).ToListAsync();
        }
       
        public async Task< DepartmentDetailsToReturnDTO?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
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

        public async Task<int> CreatedDepartmentAsync(CreatedDepartmentDTO departmentDTO)
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
            _unitOfWork.DepartmentRepository.Add(CreatedDapartment);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateDepartmentAsync(UpdatedDepartmentDTO departmentDTO)
        {
            var updatedDapartment = new Department()
            {
                Id= departmentDTO.Id,
                Code = departmentDTO.Code,
                Name = departmentDTO.Name,
                Description = departmentDTO.Description,
                CreationDate = departmentDTO.CreationDate,
                CreatedBy = 1,
                LastModificationBy = 1,
                LastModificationOn = DateTime.UtcNow,
            };
            _unitOfWork.DepartmentRepository.Update(updatedDapartment);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department is not null)
            {
                _unitOfWork.DepartmentRepository.Delete(department);
            }
            return await _unitOfWork.CompleteAsync() > 0;
        }

    }
}
