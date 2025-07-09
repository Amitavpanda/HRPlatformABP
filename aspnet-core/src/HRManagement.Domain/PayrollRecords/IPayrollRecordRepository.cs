using HRManagement;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HRManagement.PayrollRecords
{
    public partial interface IPayrollRecordRepository : IRepository<PayrollRecord, Guid>
    {

        Task DeleteAllAsync(
            string? filterText = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null,
            decimal? baseSalaryMin = null,
            decimal? baseSalaryMax = null,
            decimal? leaveDeductionsMin = null,
            decimal? leaveDeductionsMax = null,
            decimal? netPayMin = null,
            decimal? netPayMax = null,
            PayrollRecordStatus? status = null,
            string? payslipUrl = null,
            Guid? employeeId = null,
            CancellationToken cancellationToken = default);
        Task<PayrollRecordWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );

        Task<List<PayrollRecordWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null,
            decimal? baseSalaryMin = null,
            decimal? baseSalaryMax = null,
            decimal? leaveDeductionsMin = null,
            decimal? leaveDeductionsMax = null,
            decimal? netPayMin = null,
            decimal? netPayMax = null,
            PayrollRecordStatus? status = null,
            string? payslipUrl = null,
            Guid? employeeId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<PayrollRecord>> GetListAsync(
                    string? filterText = null,
                    int? monthMin = null,
                    int? monthMax = null,
                    int? yearMin = null,
                    int? yearMax = null,
                    decimal? baseSalaryMin = null,
                    decimal? baseSalaryMax = null,
                    decimal? leaveDeductionsMin = null,
                    decimal? leaveDeductionsMax = null,
                    decimal? netPayMin = null,
                    decimal? netPayMax = null,
                    PayrollRecordStatus? status = null,
                    string? payslipUrl = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null,
            decimal? baseSalaryMin = null,
            decimal? baseSalaryMax = null,
            decimal? leaveDeductionsMin = null,
            decimal? leaveDeductionsMax = null,
            decimal? netPayMin = null,
            decimal? netPayMax = null,
            PayrollRecordStatus? status = null,
            string? payslipUrl = null,
            Guid? employeeId = null,
            CancellationToken cancellationToken = default);
    }
}