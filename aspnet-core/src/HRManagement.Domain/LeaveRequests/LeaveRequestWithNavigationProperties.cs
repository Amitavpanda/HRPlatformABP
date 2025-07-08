using HRManagement.Employees;
using Volo.Abp.Identity;

using System;
using System.Collections.Generic;

namespace HRManagement.LeaveRequests
{
    public abstract class LeaveRequestWithNavigationPropertiesBase
    {
        public LeaveRequest LeaveRequest { get; set; } = null!;

        public Employee Employee { get; set; } = null!;
        public IdentityUser ReviewedBy { get; set; } = null!;
        

        
    }
}