using HRManagement;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HRManagement.PayrollRecords
{
    public abstract class PayrollRecordCreateDtoBase
    {
        [Range(PayrollRecordConsts.MonthMinLength, PayrollRecordConsts.MonthMaxLength)]
        public int Month { get; set; }
        [Range(PayrollRecordConsts.YearMinLength, PayrollRecordConsts.YearMaxLength)]
        public int Year { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal LeaveDeductions { get; set; }
        public decimal NetPay { get; set; }
        public PayrollRecordStatus Status { get; set; } = ((PayrollRecordStatus[])Enum.GetValues(typeof(PayrollRecordStatus)))[0];
        public string? PayslipUrl { get; set; }
        public Guid EmployeeId { get; set; }
    }
}