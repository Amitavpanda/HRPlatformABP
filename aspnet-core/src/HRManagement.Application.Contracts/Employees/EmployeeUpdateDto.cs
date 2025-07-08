using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace HRManagement.Employees
{
    public abstract class EmployeeUpdateDtoBase : IHasConcurrencyStamp
    {
        [StringLength(EmployeeConsts.EmployeeNumberMaxLength, MinimumLength = EmployeeConsts.EmployeeNumberMinLength)]
        public string? EmployeeNumber { get; set; }
        public DateTime DateOfJoining { get; set; }
        public decimal LeaveBalance { get; set; }
        public decimal BaseSalary { get; set; }
        public Guid? IdentityUserId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}