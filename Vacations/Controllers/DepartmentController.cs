using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vacations_DAL;
using Vacations_DomainModel.Models.Department;

namespace Vacations.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: DepartmentController
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        public async Task<IActionResult> Index()
        {
       //     _unitOfWork.BeginTransaction();
         var departments = new List<Department>();
            var employeeRepo = _unitOfWork.GetRepository<Department>();
            departments = employeeRepo.AsReadOnlyQueryable()
                .Include(d => d.Employees) // does not fetch the settings without the include!
                .ToList<Department>();
            return View(departments);

        }

        // GET: DepartmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            try
            {
                var userSettingsRepo = _unitOfWork.GetRepository<Department>();
                userSettingsRepo.Create(department);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DepartmentController/Edit/5
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

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DepartmentController/Delete/5
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
