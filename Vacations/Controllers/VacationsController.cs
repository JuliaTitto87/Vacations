using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Vacations.Models;
using Vacations_DAL;
using Vacations_DomainModel.Models.Department;
using Vacations_DomainModel.Models.Vacation;

namespace Vacations.Controllers
{
    public class VacationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VacationsController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public ActionResult Index(string month, int year)
        {
              _unitOfWork.BeginTransaction();
              List<Employee> vacations = new List<Employee>();
              var employeeRepo = _unitOfWork.GetRepository<Employee>();
              

            int _year = DateTime.Now.Year;

            string _month = DateTimeFormatInfo.CurrentInfo.MonthNames[DateTime.Now.Month - 1];

            employeeRepo.AsReadOnlyQueryable().Include(d => d.Department).ToList<Employee>();

            
            IQueryable<Employee> employees = employeeRepo.AsReadOnlyQueryable().Include(d => d.Vacations).ThenInclude(d => d.PartsOfVacation);

            if (month!=null && month!="")
            { 
                _month = month;
            }
            if (year!=0)
            {
                _year=year;
            }
            VacationsViewModel vacationsViewModel = new VacationsViewModel
            {
                Employees = employees,
                
                Months = new SelectList(DateTimeFormatInfo.CurrentInfo.MonthNames),
                Month = _month,
                Year = _year

            };

            return View(vacationsViewModel);
        }

        // GET: VacationsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VacationsController/Create
        public ActionResult Create(int Id)
        {
            var vacationRepo = _unitOfWork.GetRepository<Vacation>();

           Vacation vacation= vacationRepo.AsReadOnlyQueryable().Include(d => d.PartsOfVacation).FirstOrDefault(p => p.EmployeeId == Id);
            return View(vacation);
        }

        // POST: VacationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: VacationsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VacationsController/Edit/5
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

        // GET: VacationsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VacationsController/Delete/5
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
