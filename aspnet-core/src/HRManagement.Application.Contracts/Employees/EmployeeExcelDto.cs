using System;

namespace HRManagement.Employees
{
    public abstract class EmployeeExcelDtoBase
    {
        public string? EmployeeNumber { get; set; }
        public DateTime DateOfJoining { get; set; }
        public decimal PaidLeaveBalance { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal UnpaidLeaveBalance { get; set; }
        public decimal SickLeaveBalance { get; set; }
        public decimal DeductionPerDay { get; set; }
    }
}