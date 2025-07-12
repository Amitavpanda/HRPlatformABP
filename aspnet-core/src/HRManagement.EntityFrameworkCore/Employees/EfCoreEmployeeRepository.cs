using Volo.Abp.Identity;
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

namespace HRManagement.Employees
{
    public abstract class EfCoreEmployeeRepositoryBase : EfCoreRepository<HRManagementDbContext, Employee, Guid>
    {
        public EfCoreEmployeeRepositoryBase(IDbContextProvider<HRManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task DeleteAllAsync(
            string? filterText = null,
                        string? employeeNumber = null,
            DateTime? dateOfJoiningMin = null,
            DateTime? dateOfJoiningMax = null,
            decimal? paidLeaveBalanceMin = null,
            decimal? paidLeaveBalanceMax = null,
            decimal? baseSalaryMin = null,
            decimal? baseSalaryMax = null,
            decimal? unpaidLeaveBalanceMin = null,
            decimal? unpaidLeaveBalanceMax = null,
            decimal? sickLeaveBalanceMin = null,
            decimal? sickLeaveBalanceMax = null,
            decimal? deductionPerDayMin = null,
            decimal? deductionPerDayMax = null,
            Guid? identityUserId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();

            query = ApplyFilter(query, filterText, employeeNumber, dateOfJoiningMin, dateOfJoiningMax, paidLeaveBalanceMin, paidLeaveBalanceMax, baseSalaryMin, baseSalaryMax, unpaidLeaveBalanceMin, unpaidLeaveBalanceMax, sickLeaveBalanceMin, sickLeaveBalanceMax, deductionPerDayMin, deductionPerDayMax, identityUserId);

            var ids = query.Select(x => x.Employee.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<EmployeeWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(employee => new EmployeeWithNavigationProperties
                {
                    Employee = employee,
                    IdentityUser = dbContext.Set<IdentityUser>().FirstOrDefault(c => c.Id == employee.IdentityUserId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<EmployeeWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? employeeNumber = null,
            DateTime? dateOfJoiningMin = null,
            DateTime? dateOfJoiningMax = null,
            decimal? paidLeaveBalanceMin = null,
            decimal? paidLeaveBalanceMax = null,
            decimal? baseSalaryMin = null,
            decimal? baseSalaryMax = null,
            decimal? unpaidLeaveBalanceMin = null,
            decimal? unpaidLeaveBalanceMax = null,
            decimal? sickLeaveBalanceMin = null,
            decimal? sickLeaveBalanceMax = null,
            decimal? deductionPerDayMin = null,
            decimal? deductionPerDayMax = null,
            Guid? identityUserId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, employeeNumber, dateOfJoiningMin, dateOfJoiningMax, paidLeaveBalanceMin, paidLeaveBalanceMax, baseSalaryMin, baseSalaryMax, unpaidLeaveBalanceMin, unpaidLeaveBalanceMax, sickLeaveBalanceMin, sickLeaveBalanceMax, deductionPerDayMin, deductionPerDayMax, identityUserId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<EmployeeWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from employee in (await GetDbSetAsync())
                   join identityUser in (await GetDbContextAsync()).Set<IdentityUser>() on employee.IdentityUserId equals identityUser.Id into users
                   from identityUser in users.DefaultIfEmpty()
                   select new EmployeeWithNavigationProperties
                   {
                       Employee = employee,
                       IdentityUser = identityUser
                   };
        }

        protected virtual IQueryable<EmployeeWithNavigationProperties> ApplyFilter(
            IQueryable<EmployeeWithNavigationProperties> query,
            string? filterText,
            string? employeeNumber = null,
            DateTime? dateOfJoiningMin = null,
            DateTime? dateOfJoiningMax = null,
            decimal? paidLeaveBalanceMin = null,
            decimal? paidLeaveBalanceMax = null,
            decimal? baseSalaryMin = null,
            decimal? baseSalaryMax = null,
            decimal? unpaidLeaveBalanceMin = null,
            decimal? unpaidLeaveBalanceMax = null,
            decimal? sickLeaveBalanceMin = null,
            decimal? sickLeaveBalanceMax = null,
            decimal? deductionPerDayMin = null,
            decimal? deductionPerDayMax = null,
            Guid? identityUserId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Employee.EmployeeNumber!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(employeeNumber), e => e.Employee.EmployeeNumber.Contains(employeeNumber))
                    .WhereIf(dateOfJoiningMin.HasValue, e => e.Employee.DateOfJoining >= dateOfJoiningMin!.Value)
                    .WhereIf(dateOfJoiningMax.HasValue, e => e.Employee.DateOfJoining <= dateOfJoiningMax!.Value)
                    .WhereIf(paidLeaveBalanceMin.HasValue, e => e.Employee.PaidLeaveBalance >= paidLeaveBalanceMin!.Value)
                    .WhereIf(paidLeaveBalanceMax.HasValue, e => e.Employee.PaidLeaveBalance <= paidLeaveBalanceMax!.Value)
                    .WhereIf(baseSalaryMin.HasValue, e => e.Employee.BaseSalary >= baseSalaryMin!.Value)
                    .WhereIf(baseSalaryMax.HasValue, e => e.Employee.BaseSalary <= baseSalaryMax!.Value)
                    .WhereIf(unpaidLeaveBalanceMin.HasValue, e => e.Employee.UnpaidLeaveBalance >= unpaidLeaveBalanceMin!.Value)
                    .WhereIf(unpaidLeaveBalanceMax.HasValue, e => e.Employee.UnpaidLeaveBalance <= unpaidLeaveBalanceMax!.Value)
                    .WhereIf(sickLeaveBalanceMin.HasValue, e => e.Employee.SickLeaveBalance >= sickLeaveBalanceMin!.Value)
                    .WhereIf(sickLeaveBalanceMax.HasValue, e => e.Employee.SickLeaveBalance <= sickLeaveBalanceMax!.Value)
                    .WhereIf(deductionPerDayMin.HasValue, e => e.Employee.DeductionPerDay >= deductionPerDayMin!.Value)
                    .WhereIf(deductionPerDayMax.HasValue, e => e.Employee.DeductionPerDay <= deductionPerDayMax!.Value)
                    .WhereIf(identityUserId != null && identityUserId != Guid.Empty, e => e.IdentityUser != null && e.IdentityUser.Id == identityUserId);
        }

        public virtual async Task<List<Employee>> GetListAsync(
            string? filterText = null,
            string? employeeNumber = null,
            DateTime? dateOfJoiningMin = null,
            DateTime? dateOfJoiningMax = null,
            decimal? paidLeaveBalanceMin = null,
            decimal? paidLeaveBalanceMax = null,
            decimal? baseSalaryMin = null,
            decimal? baseSalaryMax = null,
            decimal? unpaidLeaveBalanceMin = null,
            decimal? unpaidLeaveBalanceMax = null,
            decimal? sickLeaveBalanceMin = null,
            decimal? sickLeaveBalanceMax = null,
            decimal? deductionPerDayMin = null,
            decimal? deductionPerDayMax = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, employeeNumber, dateOfJoiningMin, dateOfJoiningMax, paidLeaveBalanceMin, paidLeaveBalanceMax, baseSalaryMin, baseSalaryMax, unpaidLeaveBalanceMin, unpaidLeaveBalanceMax, sickLeaveBalanceMin, sickLeaveBalanceMax, deductionPerDayMin, deductionPerDayMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? employeeNumber = null,
            DateTime? dateOfJoiningMin = null,
            DateTime? dateOfJoiningMax = null,
            decimal? paidLeaveBalanceMin = null,
            decimal? paidLeaveBalanceMax = null,
            decimal? baseSalaryMin = null,
            decimal? baseSalaryMax = null,
            decimal? unpaidLeaveBalanceMin = null,
            decimal? unpaidLeaveBalanceMax = null,
            decimal? sickLeaveBalanceMin = null,
            decimal? sickLeaveBalanceMax = null,
            decimal? deductionPerDayMin = null,
            decimal? deductionPerDayMax = null,
            Guid? identityUserId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, employeeNumber, dateOfJoiningMin, dateOfJoiningMax, paidLeaveBalanceMin, paidLeaveBalanceMax, baseSalaryMin, baseSalaryMax, unpaidLeaveBalanceMin, unpaidLeaveBalanceMax, sickLeaveBalanceMin, sickLeaveBalanceMax, deductionPerDayMin, deductionPerDayMax, identityUserId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Employee> ApplyFilter(
            IQueryable<Employee> query,
            string? filterText = null,
            string? employeeNumber = null,
            DateTime? dateOfJoiningMin = null,
            DateTime? dateOfJoiningMax = null,
            decimal? paidLeaveBalanceMin = null,
            decimal? paidLeaveBalanceMax = null,
            decimal? baseSalaryMin = null,
            decimal? baseSalaryMax = null,
            decimal? unpaidLeaveBalanceMin = null,
            decimal? unpaidLeaveBalanceMax = null,
            decimal? sickLeaveBalanceMin = null,
            decimal? sickLeaveBalanceMax = null,
            decimal? deductionPerDayMin = null,
            decimal? deductionPerDayMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.EmployeeNumber!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(employeeNumber), e => e.EmployeeNumber.Contains(employeeNumber))
                    .WhereIf(dateOfJoiningMin.HasValue, e => e.DateOfJoining >= dateOfJoiningMin!.Value)
                    .WhereIf(dateOfJoiningMax.HasValue, e => e.DateOfJoining <= dateOfJoiningMax!.Value)
                    .WhereIf(paidLeaveBalanceMin.HasValue, e => e.PaidLeaveBalance >= paidLeaveBalanceMin!.Value)
                    .WhereIf(paidLeaveBalanceMax.HasValue, e => e.PaidLeaveBalance <= paidLeaveBalanceMax!.Value)
                    .WhereIf(baseSalaryMin.HasValue, e => e.BaseSalary >= baseSalaryMin!.Value)
                    .WhereIf(baseSalaryMax.HasValue, e => e.BaseSalary <= baseSalaryMax!.Value)
                    .WhereIf(unpaidLeaveBalanceMin.HasValue, e => e.UnpaidLeaveBalance >= unpaidLeaveBalanceMin!.Value)
                    .WhereIf(unpaidLeaveBalanceMax.HasValue, e => e.UnpaidLeaveBalance <= unpaidLeaveBalanceMax!.Value)
                    .WhereIf(sickLeaveBalanceMin.HasValue, e => e.SickLeaveBalance >= sickLeaveBalanceMin!.Value)
                    .WhereIf(sickLeaveBalanceMax.HasValue, e => e.SickLeaveBalance <= sickLeaveBalanceMax!.Value)
                    .WhereIf(deductionPerDayMin.HasValue, e => e.DeductionPerDay >= deductionPerDayMin!.Value)
                    .WhereIf(deductionPerDayMax.HasValue, e => e.DeductionPerDay <= deductionPerDayMax!.Value);
        }
    }
}