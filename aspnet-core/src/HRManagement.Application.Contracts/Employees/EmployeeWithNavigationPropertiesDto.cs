using Volo.Abp.Identity;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace HRManagement.Employees
{
    public abstract class EmployeeWithNavigationPropertiesDtoBase
    {
        public EmployeeDto Employee { get; set; } = null!;

        public IdentityUserDto IdentityUser { get; set; } = null!;

    }
}