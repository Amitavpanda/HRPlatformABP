using HRManagement;
using Volo.Abp.Application.Dtos;
using System;

namespace HRManagement.PayrollRecords
{
    public abstract class GetPayrollRecordsInputBase : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public int? MonthMin { get; set; }
        public int? MonthMax { get; set; }
        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public decimal? BaseSalaryMin { get; set; }
        public decimal? BaseSalaryMax { get; set; }
        public decimal? LeaveDeductionsMin { get; set; }
        public decimal? LeaveDeductionsMax { get; set; }
        public decimal? NetPayMin { get; set; }
        public decimal? NetPayMax { get; set; }
        public PayrollRecordStatus? Status { get; set; }
        public string? PayslipUrl { get; set; }
        public Guid? EmployeeId { get; set; }

        public GetPayrollRecordsInputBase()
        {

        }
    }
}