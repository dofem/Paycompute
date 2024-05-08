using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using PayCompute.Entity;
using PayCompute.Models;
using PayCompute.Services.Interface;

namespace PayCompute.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment hostingEnvironment) 
        {
            _employeeService = employeeService;
            _hostingEnvironment = hostingEnvironment;
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

        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeCreateViewModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = model.Id,
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    FullName = model.FullName,
                    Gender = model.Gender,
                    Email = model.Email,
                    DOB = model.DOB,
                    DateJoined = model.DateJoined,
                    NationalInsuranceNo = model.NationalInsuranceNo,
                    PaymentMethod = model.PaymentMethod,
                    StudentLoan = model.StudentLoan,
                    UnionMember = model.UnionMember,
                    Address = model.Address,
                    City = model.City,
                    PhoneNumber = model.PhoneNumber,
                    Postcode = model.Postcode,
                    Designation = model.Designation,
                };
                if(model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uploadDir = @"images/employee";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.Now.ToString("yymmssfff")+fileName+extension;
                    var path = Path.Combine(webRootPath,uploadDir, fileName);
                    model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/"+uploadDir+"/"+fileName;
                }
                _employeeService.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            var model = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                Email = employee.Email,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                UnionMember = employee.UnionMember,
                Address = employee.Address,
                City = employee.City,
                PhoneNumber = employee.PhoneNumber,
                Postcode = employee.Postcode,
                Designation = employee.Designation,
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetById(model.Id);
                if (employee == null)
                { 
                    return NotFound();
                }
                employee.EmployeeNo = model.EmployeeNo;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.MiddleName = model.MiddleName;
                employee.NationalInsuranceNo = model.NationalInsuranceNo;
                employee.Gender = model.Gender;
                employee.Email = model.Email;
                employee.DOB =model.DOB;
                employee.DateJoined = model.DateJoined;
                employee.PhoneNumber = model.PhoneNumber;
                employee.Designation = model.Designation;
                employee.PaymentMethod = model.PaymentMethod;
                employee.StudentLoan = model.StudentLoan;
                employee.UnionMember = model.UnionMember;
                employee.Address = model.Address;
                employee.City = model.City;
                employee.Postcode = model.Postcode;
                if(model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uploadDir = @"images/employee";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.Now.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDir + "/" + fileName;
                }
                await _employeeService.UpdateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
}
