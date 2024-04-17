using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacations_DomainModel.Models.Department;

namespace Vacations_DAL.DbEntityConfigurations.Department
{
     internal class DepartmentDBConfiguration : IEntityTypeConfiguration<Vacations_DomainModel.Models.Department.Employee>
    {
       public void Configure(EntityTypeBuilder<Vacations_DomainModel.Models.Department.Employee> builder)
        {
            /*  builder.HasIndex(product => product.PersonnelNumber)
                  .IncludeProperties(
                      nameof(Vacations_DomainModel.Models.Department.Department.Id)
                  );*/
            builder.HasOne(fv => fv.Department)
              .WithMany(ft => ft.Employees);
            }
        }
    }

