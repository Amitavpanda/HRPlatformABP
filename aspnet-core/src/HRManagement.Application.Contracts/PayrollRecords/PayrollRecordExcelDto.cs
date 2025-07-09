using HRManagement;
using System;

namespace HRManagement.PayrollRecords
{
    public abstract class PayrollRecordExcelDtoBase
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal LeaveDeductions { get; set; }
        public decimal NetPay { get; set; }
        public PayrollRecordStatus Status { get; set; }
        public string? PayslipUrl { get; set; }
    }
}