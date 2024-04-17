using Microsoft.AspNetCore.Mvc.Rendering;
using Vacations_DomainModel.Models.Department;

namespace Vacations.Models
{
    public class VacationsViewModel
    { 
        public SelectList Months { get; set; }

        public string Month { get; set; }

        public int Year { get; set; }    
        public Department Department { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
    }
}
