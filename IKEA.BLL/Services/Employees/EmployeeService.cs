using IKEA.BLL.Models.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Repositories.Employees;
using IKEA.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees(string search)
        {
            return _unitOfWork.EmployeeRepository.GetAllAsQuerable()
                .Where(e => !e.IsDeleted && (string.IsNullOrEmpty(search)|| e.Name.ToLower().Contains(search.ToLower())))
                .Include(e => e.Department)
                .Select(employee => new EmployeeDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    Gender = employee.Gender.ToString(),
                    EmployeeType = employee.EmployeeType.ToString(),
                    Department = employee.Department.Name
                });
            //var employees = _employeeRepository.GetAll();
            //foreach (var employee in employees)
            //{
            //    yield return new EmployeeDto
            //    {
            //        Id = employee.Id,
            //        Name = employee.Name,
            //        Age = employee.Age,
            //        IsActive = employee.IsActive,
            //        Salary = employee.Salary,
            //        Email = employee.Email,
            //        Gender = employee.Gender.ToString(),
            //        EmployeeType = employee.EmployeeType.ToString(),
            //        Department = employee.Department.Name
            //    };
            //}
        }
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is { })
            {
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HirringDate = employee.HirringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    Department= employee.Department.Name
                };
            }
            return null;
        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HirringDate = employeeDto.HirringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
                LastModificationBy = 1,
                LastModificationOn = DateTime.UtcNow,
            };
            _unitOfWork.EmployeeRepository.Add(employee);
            return _unitOfWork.Complete();

        }
        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HirringDate = employeeDto.HirringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
                LastModificationBy = 1,
                LastModificationOn = DateTime.UtcNow,
            };
            _unitOfWork.EmployeeRepository.Update(employee);
            return _unitOfWork.Complete();
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is { })
            {
                _unitOfWork.EmployeeRepository.Delete(employee);
            }
            return _unitOfWork.Complete() > 0;
        }
    }
}
