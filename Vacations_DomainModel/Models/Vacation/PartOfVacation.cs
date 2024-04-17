using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacations_DomainModel.Models.Vacation
{
    public class PartOfVacation
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; } 
        public DateTime DateEnd { get; set; } = DateTime.MinValue;

        public int DurationOfVacationPart
        {
            get
            {
                if ((DateStart == DateTime.MinValue || DateEnd == DateTime.MinValue))
                {
                    return 0;
                }
                return (DateEnd - DateStart).Days;
            }
        }
        public Vacation Vacation { get; set; }
        public int VacationId { get; set; }
    }
}
