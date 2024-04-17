using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vacations_DomainModel.Models.Department;

namespace Vacations_DAL.DbEntityConfigurations.Department
{
    internal class EmployeeDBConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

                /*  builder.HasIndex(product => product.PersonnelNumber)
                      .IncludeProperties(
                          nameof(Vacations_DomainModel.Models.Department.Department.Id)
                      );*/
                builder.HasOne(fv => fv.Department)
                  .WithMany(ft => ft.Employees);
            
        

        builder.HasIndex(employee => employee.PersonnelNumber)
    .IncludeProperties(
        nameof(Employee.Id),
        nameof(Employee.CurrentDurationOfVocation),
        nameof(Employee.DepartmentId),
        nameof(Employee.EmployeesPosition),
        nameof(Employee.FirstName),
        nameof(Employee.LastName),
        nameof(Employee.IsHeadOfDepartment)
    );

            /*    builder.HasKey(prf => new {  prf.DepartmentId });

                builder.HasOne(prf => prf.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(prf => prf.DepartmentId);*/
        }
    }
}
