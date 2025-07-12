using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HRManagement.Employees
{
    public abstract class EmployeeCreateDtoBase
    {
        [StringLength(EmployeeConsts.EmployeeNumberMaxLength, MinimumLength = EmployeeConsts.EmployeeNumberMinLength)]
        public string? EmployeeNumber { get; set; }
        public DateTime DateOfJoining { get; set; }
        public decimal PaidLeaveBalance { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal UnpaidLeaveBalance { get; set; }
        public decimal SickLeaveBalance { get; set; }
        public decimal DeductionPerDay { get; set; }
        public Guid? IdentityUserId { get; set; }
    }
}