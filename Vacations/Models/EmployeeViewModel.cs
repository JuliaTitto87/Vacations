using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Vacations_DomainModel.Models.Department;

namespace Vacations.Models
{
    public class EmployeeViewModel
    {
        [RegularExpression("0000000", ErrorMessage = "Поле должно состоять из 7 цифр")]
        public required int PersonnelNumber { get; set; }

        [MaxLength(100, ErrorMessage = "Максимальная  длина текста - 100 символов")]

        [Required(ErrorMessage = "Поле Должность не может быть пустым")]
        public required string EmployeesPosition { get; set; }

        [MaxLength(100)]
        public required string FirstName { get; set; }


        [MaxLength(100)]
        public string? LastName { get; set; }
        public SelectList Departments { get; set; }

        public Department Department { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        public int CurrentDurationOfVocation { get; set; }
        public bool IsHeadOfDepartment { get; set; }
    }
}
