using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Vacations.Models;
using Vacations_DAL;
using Vacations_DomainModel.Models.Department;
using Vacations_DomainModel.Models.Identity;

namespace Vacations.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        // GET: EmployeesController
        public ActionResult Index()
        {
            _unitOfWork.BeginTransaction();
            List<Employee> employees = new List<Employee>();
            var employeeRepo = _unitOfWork.GetRepository<Employee>();
            employees = employeeRepo.AsReadOnlyQueryable().Include(d => d.Department).ToList<Employee>();
            return View(employees);
        }

        // GET: EmployeesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            _unitOfWork.BeginTransaction();
            var employeeRepo = _unitOfWork.GetRepository<Department>();
            IQueryable<Department> departments = employeeRepo.AsReadOnlyQueryable();

            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                Departments = new SelectList(departments, "Id", "Name"),
            };
            return View(employeeViewModel);
        }


        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee, int department)
        {

            if (ModelState.IsValid)
            {
                var rep = _unitOfWork.GetRepository<Department>();
                IQueryable<Department> departments = rep.AsReadOnlyQueryable();
                employee.DepartmentId = department;
                var userSettingsRepo = _unitOfWork.GetRepository<Employee>();
                userSettingsRepo.Create(employee);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            {
                var employeeRepo = _unitOfWork.GetRepository<Department>();
                IQueryable<Department> departments = employeeRepo.AsReadOnlyQueryable();
                EmployeeViewModel employeeViewModel = new EmployeeViewModel
                {
                    CurrentDurationOfVocation = employee.CurrentDurationOfVocation,
                    Department = employee.Department,
                    IsHeadOfDepartment = employee.IsHeadOfDepartment,
                    LastName = employee.LastName,
                    EmployeesPosition = employee.EmployeesPosition,
                    FirstName = employee.FirstName,
                    PersonnelNumber = employee.PersonnelNumber,
                    Departments = new SelectList(departments, "Id", "Name"),
                };
                return View(employeeViewModel);
            }

        }

        // GET: EmployeesController/Edit/5
        public ActionResult Edit(int id)
        {
            _unitOfWork.BeginTransaction();
            var departmentRepo = _unitOfWork.GetRepository<Department>();
            IQueryable<Department> departments = departmentRepo.AsReadOnlyQueryable();

            var employeeRepo = _unitOfWork.GetRepository<Employee>();
            IQueryable<Employee> employees = employeeRepo.AsReadOnlyQueryable().Include(d => d.Department);

            Employee employee = employees.Include(d => d.Department).FirstOrDefault(_employee => _employee.Id == id);

            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                Departments = new SelectList(departments, "Id", "Name"),
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                IsHeadOfDepartment = employee.IsHeadOfDepartment,
                CurrentDurationOfVocation = employee.CurrentDurationOfVocation,
                Department = employee.Department,
                EmployeesPosition = employee.EmployeesPosition,
                PersonnelNumber = employee.PersonnelNumber
            };
            return View(employeeViewModel);
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee, int department)
        
        { var rep = _unitOfWork.GetRepository<Department>();
                IQueryable<Department> departments = rep.AsReadOnlyQueryable().Include(e=>e.Employees);
                employee.DepartmentId = department;
            employee.Department = departments.FirstOrDefault(d => d.Id == department);
            if (ModelState.IsValid)
            {
                
                
                var userSettingsRepo = _unitOfWork.GetRepository<Employee>();
                await userSettingsRepo.InsertOrUpdate(_employee=>_employee.Id==employee.Id, employee);
                _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            {

                EmployeeViewModel employeeViewModel = new EmployeeViewModel
                {
                    CurrentDurationOfVocation = employee.CurrentDurationOfVocation,
                    Department = employee.Department,
                    IsHeadOfDepartment = employee.IsHeadOfDepartment,
                    LastName = employee.LastName,
                    EmployeesPosition = employee.EmployeesPosition,
                    FirstName = employee.FirstName,
                    PersonnelNumber = employee.PersonnelNumber,
                    Departments = new SelectList(departments, "Id", "Name"),
                };
                return View(employeeViewModel);
            }
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
