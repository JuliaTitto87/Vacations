using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacations_DomainModel.Models.Vacation
{
    public class Vacation:Entity<int>
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Duration { get; set;} 

        public List<PartOfVacation> PartsOfVacation { get; set;}

        public required int EmployeeId { get; set; }
        public required Department.Employee Employee { get; set; }
    }
}
