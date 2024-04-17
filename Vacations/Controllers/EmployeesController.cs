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
            employees =  employeeRepo.AsReadOnlyQueryable().ToList<Employee>();
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
                EmployeesPosition = "",
                FirstName = "",
                PersonnelNumber = 0000000,
                Departments = new SelectList(departments,"Id", "Name" )


            };




            return View(employeeViewModel);
        }


        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            try
            {
                var userSettingsRepo = _unitOfWork.GetRepository<Employee>();
                userSettingsRepo.Create(employee);
              await  _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: EmployeesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
