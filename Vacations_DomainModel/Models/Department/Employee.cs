using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacations_DomainModel.Models.Vacation;

namespace Vacations_DomainModel.Models.Department
{
    public class Employee : Entity<int>
    {

        [Required]


        [RegularExpression("0000000", ErrorMessage = "Поле должно состоять из 7 цифр")]
        public required int PersonnelNumber { get; set; }

        [MaxLength(100, ErrorMessage = "Максимальная  длина текста - 100 символов")]

        [Required(ErrorMessage = "Поле Должность не может быть пустым")]
        public required string EmployeesPosition { get; set; }

        [MaxLength(100)]
        public required string FirstName { get; set; }


        [MaxLength(100)]
        public string? LastName { get; set; }

        public string FullName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LastName))
                {
                    return FirstName;
                }
                return $"{FirstName} {LastName}";
            }
        }
        public int CurrentDurationOfVocation {  get; set; }
        public List<Vacation.Vacation>? Vacations { get; set; }
        [ForeignKey("Department")]
        public required int DepartmentId { get; set; }
        public required Department Department { get; set; }

        public bool IsHeadOfDepartment { get; set; }

    }
}
