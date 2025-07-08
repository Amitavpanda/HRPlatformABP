using System;

namespace HRManagement.Employees
{
    public abstract class EmployeeExcelDtoBase
    {
        public string? EmployeeNumber { get; set; }
        public DateTime DateOfJoining { get; set; }
        public decimal LeaveBalance { get; set; }
        public decimal BaseSalary { get; set; }
    }
}