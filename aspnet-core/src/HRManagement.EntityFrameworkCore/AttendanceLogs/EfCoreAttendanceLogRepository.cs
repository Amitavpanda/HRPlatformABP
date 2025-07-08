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

namespace HRManagement.AttendanceLogs
{
    public abstract class EfCoreAttendanceLogRepositoryBase : EfCoreRepository<HRManagementDbContext, AttendanceLog, Guid>
    {
        public EfCoreAttendanceLogRepositoryBase(IDbContextProvider<HRManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task DeleteAllAsync(
            string? filterText = null,
                        DateTime? dateMin = null,
            DateTime? dateMax = null,
            TimeOnly? checkInTimeMin = null,
            TimeOnly? checkInTimeMax = null,
            TimeOnly? checkOutTimeMin = null,
            TimeOnly? checkOutTimeMax = null,
            AttendanceStatus? status = null,
            Guid? employeeId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();

            query = ApplyFilter(query, filterText, dateMin, dateMax, checkInTimeMin, checkInTimeMax, checkOutTimeMin, checkOutTimeMax, status, employeeId);

            var ids = query.Select(x => x.AttendanceLog.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<AttendanceLogWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(attendanceLog => new AttendanceLogWithNavigationProperties
                {
                    AttendanceLog = attendanceLog,
                    Employee = dbContext.Set<Employee>().FirstOrDefault(c => c.Id == attendanceLog.EmployeeId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<AttendanceLogWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            TimeOnly? checkInTimeMin = null,
            TimeOnly? checkInTimeMax = null,
            TimeOnly? checkOutTimeMin = null,
            TimeOnly? checkOutTimeMax = null,
            AttendanceStatus? status = null,
            Guid? employeeId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, dateMin, dateMax, checkInTimeMin, checkInTimeMax, checkOutTimeMin, checkOutTimeMax, status, employeeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AttendanceLogConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<AttendanceLogWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from attendanceLog in (await GetDbSetAsync())
                   join employee in (await GetDbContextAsync()).Set<Employee>() on attendanceLog.EmployeeId equals employee.Id into employees
                   from employee in employees.DefaultIfEmpty()
                   select new AttendanceLogWithNavigationProperties
                   {
                       AttendanceLog = attendanceLog,
                       Employee = employee
                   };
        }

        protected virtual IQueryable<AttendanceLogWithNavigationProperties> ApplyFilter(
            IQueryable<AttendanceLogWithNavigationProperties> query,
            string? filterText,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            TimeOnly? checkInTimeMin = null,
            TimeOnly? checkInTimeMax = null,
            TimeOnly? checkOutTimeMin = null,
            TimeOnly? checkOutTimeMax = null,
            AttendanceStatus? status = null,
            Guid? employeeId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(dateMin.HasValue, e => e.AttendanceLog.Date >= dateMin!.Value)
                    .WhereIf(dateMax.HasValue, e => e.AttendanceLog.Date <= dateMax!.Value)
                    .WhereIf(checkInTimeMin.HasValue, e => e.AttendanceLog.CheckInTime >= checkInTimeMin!.Value)
                    .WhereIf(checkInTimeMax.HasValue, e => e.AttendanceLog.CheckInTime <= checkInTimeMax!.Value)
                    .WhereIf(checkOutTimeMin.HasValue, e => e.AttendanceLog.CheckOutTime >= checkOutTimeMin!.Value)
                    .WhereIf(checkOutTimeMax.HasValue, e => e.AttendanceLog.CheckOutTime <= checkOutTimeMax!.Value)
                    .WhereIf(status.HasValue, e => e.AttendanceLog.Status == status)
                    .WhereIf(employeeId != null && employeeId != Guid.Empty, e => e.Employee != null && e.Employee.Id == employeeId);
        }

        public virtual async Task<List<AttendanceLog>> GetListAsync(
            string? filterText = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            TimeOnly? checkInTimeMin = null,
            TimeOnly? checkInTimeMax = null,
            TimeOnly? checkOutTimeMin = null,
            TimeOnly? checkOutTimeMax = null,
            AttendanceStatus? status = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, dateMin, dateMax, checkInTimeMin, checkInTimeMax, checkOutTimeMin, checkOutTimeMax, status);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AttendanceLogConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            TimeOnly? checkInTimeMin = null,
            TimeOnly? checkInTimeMax = null,
            TimeOnly? checkOutTimeMin = null,
            TimeOnly? checkOutTimeMax = null,
            AttendanceStatus? status = null,
            Guid? employeeId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, dateMin, dateMax, checkInTimeMin, checkInTimeMax, checkOutTimeMin, checkOutTimeMax, status, employeeId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<AttendanceLog> ApplyFilter(
            IQueryable<AttendanceLog> query,
            string? filterText = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            TimeOnly? checkInTimeMin = null,
            TimeOnly? checkInTimeMax = null,
            TimeOnly? checkOutTimeMin = null,
            TimeOnly? checkOutTimeMax = null,
            AttendanceStatus? status = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(dateMin.HasValue, e => e.Date >= dateMin!.Value)
                    .WhereIf(dateMax.HasValue, e => e.Date <= dateMax!.Value)
                    .WhereIf(checkInTimeMin.HasValue, e => e.CheckInTime >= checkInTimeMin!.Value)
                    .WhereIf(checkInTimeMax.HasValue, e => e.CheckInTime <= checkInTimeMax!.Value)
                    .WhereIf(checkOutTimeMin.HasValue, e => e.CheckOutTime >= checkOutTimeMin!.Value)
                    .WhereIf(checkOutTimeMax.HasValue, e => e.CheckOutTime <= checkOutTimeMax!.Value)
                    .WhereIf(status.HasValue, e => e.Status == status);
        }
    }
}