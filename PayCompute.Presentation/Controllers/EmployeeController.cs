using Microsoft.AspNetCore.Mvc;
using PayCompute.Models;
using PayCompute.Services.Interface;

namespace PayCompute.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) 
        {
            _employeeService = employeeService;
        }
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees().Select(employee => new EmployeeIndexViewModel
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                ImageUrl = employee.ImageUrl,
                FullName = employee.FullName,
                Gender = employee.Gender,
                Designation = employee.Designation,
                City = employee.City,
                DateJoined = employee.DateJoined,
            }).ToList();
            return View(employees);
        }
    }
}
