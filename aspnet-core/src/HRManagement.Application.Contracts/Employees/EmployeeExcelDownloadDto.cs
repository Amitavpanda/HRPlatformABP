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
        public decimal? LeaveBalanceMin { get; set; }
        public decimal? LeaveBalanceMax { get; set; }
        public decimal? BaseSalaryMin { get; set; }
        public decimal? BaseSalaryMax { get; set; }
        public Guid? IdentityUserId { get; set; }

        public EmployeeExcelDownloadDtoBase()
        {

        }
    }
}