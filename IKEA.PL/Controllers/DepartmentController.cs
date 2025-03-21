﻿using AutoMapper;
using IKEA.BLL.Models.Departments;
using IKEA.BLL.Services;
using IKEA.PL.Models.Departments;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        #region Services
        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger , IWebHostEnvironment environment , IMapper mapper)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
            _mapper = mapper;
        }
        #endregion
        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //ViewData["Message"] = "Hello ViewData";
            //ViewBag.Message= "Hello ViewBag";
            var departments =await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
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
        public async Task<IActionResult> Create(DepartmentEditViewModel departmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var message = string.Empty;
            try
            {
                var createdDepartment = _mapper.Map<CreatedDepartmentDTO>(departmentVM);
                var result =await _departmentService.CreatedDepartmentAsync(createdDepartment);
                if (result > 0)
                {
                    //TempData["Message"] = "Department is created";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //TempData["Message"] = "Depaartment hasn't been created";
                    message = "Sorry the Department hasn't been created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentVM);
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex,ex.Message);
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(departmentVM);
                }
                else
                {
                    message = "Sorry the Department hasn't been created";
                    return View("Error",message);
                }
            }
        }
        #endregion
        #endregion
        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int?id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }
            var department =await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department == null)
                return NotFound(); //404
            return View(department);
        }

        #endregion
        #region Edit
        #region Get
        [HttpGet]
        public async Task<IActionResult> Edit(int?id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }
            var department =await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department == null)
                return NotFound(); //404
            var departmentVM = _mapper.Map<DepartmentDetailsToReturnDTO,DepartmentEditViewModel>(department);
            return View(departmentVM); // check if instructor did that i made that
            // Mannual mapping
            //return View(new DepartmentEditViewModel()
            //{
            //    Code=department.Code,
            //    Name=department.Name,
            //    Description=department.Description,
            //    CreationDate=department.CreationDate
            //});
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id , DepartmentEditViewModel departmentVM) 
        {
            if (!ModelState.IsValid)
                return View(departmentVM);
            var message = string.Empty;
            try
            {
                // manual mapping
                //var updatedDepartment = new UpdatedDepartmentDTO()
                //{
                //    Id = id,
                //    Code = departmentVM.Code,
                //    Name = departmentVM.Name,
                //    Description = departmentVM.Description,
                //    CreationDate = departmentVM.CreationDate
                //};
                var updatedDepartment = _mapper.Map<UpdatedDepartmentDTO>(departmentVM);
                var updated =await _departmentService.UpdateDepartmentAsync(updatedDepartment) > 0;
                if (updated)
                    return RedirectToAction(nameof(Index));
                message = "Sorry, An error occured while updating the department";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                message = _environment.IsDevelopment ()?ex.Message: "Sorry, An error occured while updating the department";
            }
            ModelState.AddModelError(string.Empty,message);
            return View(departmentVM);
        }
        #endregion
        #endregion
        #region Delete
        #region Get
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }
            var department =await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department == null)
                return NotFound(); //404
            return View(department);
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deleted =await _departmentService.DeleteDepartmentAsync(id);
                if (deleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                message = "Sorry, An error occured while deleting the Department";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "Sorry, An error occured while deleting the department";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #endregion
    }
}
