using HRManagement;
using HRManagement.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using HRManagement.EntityFrameworkCore;

namespace HRManagement.PayrollRecords
{
    public abstract class EfCorePayrollRecordRepositoryBase : EfCoreRepository<HRManagementDbContext, PayrollRecord, Guid>
    {
        public EfCorePayrollRecordRepositoryBase(IDbContextProvider<HRManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task DeleteAllAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();

            query = ApplyFilter(query, filterText, monthMin, monthMax, yearMin, yearMax, baseSalaryMin, baseSalaryMax, leaveDeductionsMin, leaveDeductionsMax, netPayMin, netPayMax, status, payslipUrl, employeeId);

            var ids = query.Select(x => x.PayrollRecord.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<PayrollRecordWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(payrollRecord => new PayrollRecordWithNavigationProperties
                {
                    PayrollRecord = payrollRecord,
                    Employee = dbContext.Set<Employee>().FirstOrDefault(c => c.Id == payrollRecord.EmployeeId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<PayrollRecordWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, monthMin, monthMax, yearMin, yearMax, baseSalaryMin, baseSalaryMax, leaveDeductionsMin, leaveDeductionsMax, netPayMin, netPayMax, status, payslipUrl, employeeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PayrollRecordConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<PayrollRecordWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from payrollRecord in (await GetDbSetAsync())
                   join employee in (await GetDbContextAsync()).Set<Employee>() on payrollRecord.EmployeeId equals employee.Id into employees
                   from employee in employees.DefaultIfEmpty()
                   select new PayrollRecordWithNavigationProperties
                   {
                       PayrollRecord = payrollRecord,
                       Employee = employee
                   };
        }

        protected virtual IQueryable<PayrollRecordWithNavigationProperties> ApplyFilter(
            IQueryable<PayrollRecordWithNavigationProperties> query,
            string? filterText,
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
            Guid? employeeId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.PayrollRecord.PayslipUrl!.Contains(filterText!))
                    .WhereIf(monthMin.HasValue, e => e.PayrollRecord.Month >= monthMin!.Value)
                    .WhereIf(monthMax.HasValue, e => e.PayrollRecord.Month <= monthMax!.Value)
                    .WhereIf(yearMin.HasValue, e => e.PayrollRecord.Year >= yearMin!.Value)
                    .WhereIf(yearMax.HasValue, e => e.PayrollRecord.Year <= yearMax!.Value)
                    .WhereIf(baseSalaryMin.HasValue, e => e.PayrollRecord.BaseSalary >= baseSalaryMin!.Value)
                    .WhereIf(baseSalaryMax.HasValue, e => e.PayrollRecord.BaseSalary <= baseSalaryMax!.Value)
                    .WhereIf(leaveDeductionsMin.HasValue, e => e.PayrollRecord.LeaveDeductions >= leaveDeductionsMin!.Value)
                    .WhereIf(leaveDeductionsMax.HasValue, e => e.PayrollRecord.LeaveDeductions <= leaveDeductionsMax!.Value)
                    .WhereIf(netPayMin.HasValue, e => e.PayrollRecord.NetPay >= netPayMin!.Value)
                    .WhereIf(netPayMax.HasValue, e => e.PayrollRecord.NetPay <= netPayMax!.Value)
                    .WhereIf(status.HasValue, e => e.PayrollRecord.Status == status)
                    .WhereIf(!string.IsNullOrWhiteSpace(payslipUrl), e => e.PayrollRecord.PayslipUrl.Contains(payslipUrl))
                    .WhereIf(employeeId != null && employeeId != Guid.Empty, e => e.Employee != null && e.Employee.Id == employeeId);
        }

        public virtual async Task<List<PayrollRecord>> GetListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, monthMin, monthMax, yearMin, yearMax, baseSalaryMin, baseSalaryMax, leaveDeductionsMin, leaveDeductionsMax, netPayMin, netPayMax, status, payslipUrl);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PayrollRecordConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, monthMin, monthMax, yearMin, yearMax, baseSalaryMin, baseSalaryMax, leaveDeductionsMin, leaveDeductionsMax, netPayMin, netPayMax, status, payslipUrl, employeeId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<PayrollRecord> ApplyFilter(
            IQueryable<PayrollRecord> query,
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
            string? payslipUrl = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.PayslipUrl!.Contains(filterText!))
                    .WhereIf(monthMin.HasValue, e => e.Month >= monthMin!.Value)
                    .WhereIf(monthMax.HasValue, e => e.Month <= monthMax!.Value)
                    .WhereIf(yearMin.HasValue, e => e.Year >= yearMin!.Value)
                    .WhereIf(yearMax.HasValue, e => e.Year <= yearMax!.Value)
                    .WhereIf(baseSalaryMin.HasValue, e => e.BaseSalary >= baseSalaryMin!.Value)
                    .WhereIf(baseSalaryMax.HasValue, e => e.BaseSalary <= baseSalaryMax!.Value)
                    .WhereIf(leaveDeductionsMin.HasValue, e => e.LeaveDeductions >= leaveDeductionsMin!.Value)
                    .WhereIf(leaveDeductionsMax.HasValue, e => e.LeaveDeductions <= leaveDeductionsMax!.Value)
                    .WhereIf(netPayMin.HasValue, e => e.NetPay >= netPayMin!.Value)
                    .WhereIf(netPayMax.HasValue, e => e.NetPay <= netPayMax!.Value)
                    .WhereIf(status.HasValue, e => e.Status == status)
                    .WhereIf(!string.IsNullOrWhiteSpace(payslipUrl), e => e.PayslipUrl.Contains(payslipUrl));
        }
    }
}