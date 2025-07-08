using HRManagement.Employees;
using Volo.Abp.Identity;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace HRManagement.LeaveRequests
{
    public abstract class LeaveRequestWithNavigationPropertiesDtoBase
    {
        public LeaveRequestDto LeaveRequest { get; set; } = null!;

        public EmployeeDto Employee { get; set; } = null!;
        public IdentityUserDto ReviewedBy { get; set; } = null!;

    }
}