using Volo.Abp.Application.Dtos;
using System;

namespace HRManagement.Employees
{
    public abstract class EmployeeExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? EmployeeNumber { get; set; }
        public DateTime? DateOfJoiningMin { get; set; }
        public DateTime? DateOfJoiningMax { get; set; }
        public decimal? PaidLeaveBalanceMin { get; set; }
        public decimal? PaidLeaveBalanceMax { get; set; }
        public decimal? BaseSalaryMin { get; set; }
        public decimal? BaseSalaryMax { get; set; }
        public decimal? UnpaidLeaveBalanceMin { get; set; }
        public decimal? UnpaidLeaveBalanceMax { get; set; }
        public decimal? SickLeaveBalanceMin { get; set; }
        public decimal? SickLeaveBalanceMax { get; set; }
        public decimal? DeductionPerDayMin { get; set; }
        public decimal? DeductionPerDayMax { get; set; }
        public Guid? IdentityUserId { get; set; }

        public EmployeeExcelDownloadDtoBase()
        {

        }
    }
}