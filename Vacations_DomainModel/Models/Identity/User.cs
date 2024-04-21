using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacations_DomainModel.Models.Identity
{
    public class User : IdentityUser, IEntity
    {
        [PersonalData]
        public override string Id { get; set; } = default!;

        [PersonalData]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [PersonalData]
        [MaxLength(100)]
        public string? LastName { get; set; }

        public required UserSettings UserSettings { get; set; }

        public required UserRoleEnum Role { get; set; }

        [ProtectedPersonalData]
        public virtual int? PersonnelNumber { get; set; }
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
    }
}
