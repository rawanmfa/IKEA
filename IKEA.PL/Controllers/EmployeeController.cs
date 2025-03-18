using IKEA.BLL.Models.Departments;
using IKEA.BLL.Models.Employees;
using IKEA.BLL.Services;
using IKEA.BLL.Services.Employees;
using IKEA.PL.Models.Departments;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _environment;
        #region Services
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _environment = environment;
            _logger = logger;
        }
        #endregion
        #region Index
        [HttpGet]
        public IActionResult Index(string search)
        {
            var employees = _employeeService.GetAllEmployees(search);
            return View(employees);
        }
        #endregion
        #region Create
        #region Get
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            var message = string.Empty;
            try
            {
                var result = _employeeService.CreateEmployee(employee);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Sorry the Employee hasn't been created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(employee);
                }
                else
                {
                    message = "Sorry the Employee hasn't been created";
                    return View("Error", message);
                }
            }
        }
        #endregion
        #endregion
        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound(); //404
            return View(employee);
        }

        #endregion
        #region Edit
        #region Get
        [HttpGet]
        public IActionResult Edit(int? id , [FromServices] IDepartmentService departmentService)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound(); //404
            ViewData["Departments"] = departmentService.GetAllDepartments();
            return View(new UpdatedEmployeeDto()
            {
                Name=employee.Name,
                Address=employee.Address,
                Email=employee.Email,
                Age = employee.Age,
                Salary=employee.Salary,
                PhoneNumber=employee.PhoneNumber,
                IsActive=employee.IsActive,
                EmployeeType=employee.EmployeeType,
                Gender=employee.Gender,
                HirringDate=employee.HirringDate,
            });
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, UpdatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var message = string.Empty;
            try
            {
                var updatedEmployee = new UpdatedEmployeeDto()
                {
                    Id = id,
                    Name = employee.Name,
                    Address = employee.Address,
                    Email = employee.Email,
                    Age = employee.Age,
                    Salary = employee.Salary,
                    PhoneNumber = employee.PhoneNumber,
                    IsActive = employee.IsActive,
                    EmployeeType = employee.EmployeeType,
                    Gender = employee.Gender,
                    HirringDate = employee.HirringDate,
                };
                var updated = _employeeService.UpdateEmployee(updatedEmployee) > 0;
                if (updated)
                    return RedirectToAction(nameof(Index));
                message = "Sorry, An error occured while updating the employee";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "Sorry, An error occured while updating the employee";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(employee);
        }
        #endregion
        #endregion
        #region Delete
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deleted = _employeeService.DeleteEmployee(id);
                if (deleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                message = "Sorry, An error occured while deleting the Employee";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "Sorry, An error occured while deleting the employee";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #endregion

    }
}
