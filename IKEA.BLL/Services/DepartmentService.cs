using IKEA.BLL.Models.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Repositories.Departments;
using IKEA.DAL.Presistance.UnitOfWork;
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

        public IEnumerable<DepartmentToReturnDTO> GetAllDepartments() // this function different from what the instructor did if error occured or the retured data look different look MVC session4 second video at 0:30
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
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
            var department = _unitOfWork.DepartmentRepository.GetById(id);
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
            _unitOfWork.DepartmentRepository.Add(CreatedDapartment);
            return _unitOfWork.Complete();
        }

        public int UpdateDepartment(UpdatedDepartmentDTO departmentDTO)
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
            return _unitOfWork.Complete();
        }
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is not null)
            {
                _unitOfWork.DepartmentRepository.Delete(department);
            }
            return _unitOfWork.Complete() > 0;
        }

    }
}
