using Volo.Abp.Identity;

using System;
using System.Collections.Generic;

namespace HRManagement.Employees
{
    public abstract class EmployeeWithNavigationPropertiesBase
    {
        public Employee Employee { get; set; } = null!;

        public IdentityUser IdentityUser { get; set; } = null!;
        

        
    }
}