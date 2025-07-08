using HRManagement;
using Volo.Abp.Identity;
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

namespace HRManagement.LeaveRequests
{
    public abstract class EfCoreLeaveRequestRepositoryBase : EfCoreRepository<HRManagementDbContext, LeaveRequest, Guid>
    {
        public EfCoreLeaveRequestRepositoryBase(IDbContextProvider<HRManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task DeleteAllAsync(
            string? filterText = null,
                        LeaveRequestType? leaveRequestType = null,
            DateTime? startDateMin = null,
            DateTime? startDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            LeaveRequestStatus? leaveRequestStatus = null,
            DateTime? requestedOnMin = null,
            DateTime? requestedOnMax = null,
            DateTime? reviewedOnMin = null,
            DateTime? reviewedOnMax = null,
            string? workflowInstanceId = null,
            Guid? employeeId = null,
            Guid? reviewedBy = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();

            query = ApplyFilter(query, filterText, leaveRequestType, startDateMin, startDateMax, endDateMin, endDateMax, leaveRequestStatus, requestedOnMin, requestedOnMax, reviewedOnMin, reviewedOnMax, workflowInstanceId, employeeId, reviewedBy);

            var ids = query.Select(x => x.LeaveRequest.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<LeaveRequestWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(leaveRequest => new LeaveRequestWithNavigationProperties
                {
                    LeaveRequest = leaveRequest,
                    Employee = dbContext.Set<Employee>().FirstOrDefault(c => c.Id == leaveRequest.EmployeeId),
                    ReviewedBy = dbContext.Set<IdentityUser>().FirstOrDefault(c => c.Id == leaveRequest.ReviewedBy)
                }).FirstOrDefault();
        }

        public virtual async Task<List<LeaveRequestWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            LeaveRequestType? leaveRequestType = null,
            DateTime? startDateMin = null,
            DateTime? startDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            LeaveRequestStatus? leaveRequestStatus = null,
            DateTime? requestedOnMin = null,
            DateTime? requestedOnMax = null,
            DateTime? reviewedOnMin = null,
            DateTime? reviewedOnMax = null,
            string? workflowInstanceId = null,
            Guid? employeeId = null,
            Guid? reviewedBy = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, leaveRequestType, startDateMin, startDateMax, endDateMin, endDateMax, leaveRequestStatus, requestedOnMin, requestedOnMax, reviewedOnMin, reviewedOnMax, workflowInstanceId, employeeId, reviewedBy);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? LeaveRequestConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<LeaveRequestWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from leaveRequest in (await GetDbSetAsync())
                   join employee in (await GetDbContextAsync()).Set<Employee>() on leaveRequest.EmployeeId equals employee.Id into employees
                   from employee in employees.DefaultIfEmpty()
                   join reviewedBy in (await GetDbContextAsync()).Set<IdentityUser>() on leaveRequest.ReviewedBy equals reviewedBy.Id into users
                   from reviewedBy in users.DefaultIfEmpty()
                   select new LeaveRequestWithNavigationProperties
                   {
                       LeaveRequest = leaveRequest,
                       Employee = employee,
                       ReviewedBy = reviewedBy
                   };
        }

        protected virtual IQueryable<LeaveRequestWithNavigationProperties> ApplyFilter(
            IQueryable<LeaveRequestWithNavigationProperties> query,
            string? filterText,
            LeaveRequestType? leaveRequestType = null,
            DateTime? startDateMin = null,
            DateTime? startDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            LeaveRequestStatus? leaveRequestStatus = null,
            DateTime? requestedOnMin = null,
            DateTime? requestedOnMax = null,
            DateTime? reviewedOnMin = null,
            DateTime? reviewedOnMax = null,
            string? workflowInstanceId = null,
            Guid? employeeId = null,
            Guid? reviewedBy = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.LeaveRequest.WorkflowInstanceId!.Contains(filterText!))
                    .WhereIf(leaveRequestType.HasValue, e => e.LeaveRequest.LeaveRequestType == leaveRequestType)
                    .WhereIf(startDateMin.HasValue, e => e.LeaveRequest.StartDate >= startDateMin!.Value)
                    .WhereIf(startDateMax.HasValue, e => e.LeaveRequest.StartDate <= startDateMax!.Value)
                    .WhereIf(endDateMin.HasValue, e => e.LeaveRequest.EndDate >= endDateMin!.Value)
                    .WhereIf(endDateMax.HasValue, e => e.LeaveRequest.EndDate <= endDateMax!.Value)
                    .WhereIf(leaveRequestStatus.HasValue, e => e.LeaveRequest.LeaveRequestStatus == leaveRequestStatus)
                    .WhereIf(requestedOnMin.HasValue, e => e.LeaveRequest.RequestedOn >= requestedOnMin!.Value)
                    .WhereIf(requestedOnMax.HasValue, e => e.LeaveRequest.RequestedOn <= requestedOnMax!.Value)
                    .WhereIf(reviewedOnMin.HasValue, e => e.LeaveRequest.ReviewedOn >= reviewedOnMin!.Value)
                    .WhereIf(reviewedOnMax.HasValue, e => e.LeaveRequest.ReviewedOn <= reviewedOnMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(workflowInstanceId), e => e.LeaveRequest.WorkflowInstanceId.Contains(workflowInstanceId))
                    .WhereIf(employeeId != null && employeeId != Guid.Empty, e => e.Employee != null && e.Employee.Id == employeeId)
                    .WhereIf(reviewedBy != null && reviewedBy != Guid.Empty, e => e.ReviewedBy != null && e.ReviewedBy.Id == reviewedBy);
        }

        public virtual async Task<List<LeaveRequest>> GetListAsync(
            string? filterText = null,
            LeaveRequestType? leaveRequestType = null,
            DateTime? startDateMin = null,
            DateTime? startDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            LeaveRequestStatus? leaveRequestStatus = null,
            DateTime? requestedOnMin = null,
            DateTime? requestedOnMax = null,
            DateTime? reviewedOnMin = null,
            DateTime? reviewedOnMax = null,
            string? workflowInstanceId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, leaveRequestType, startDateMin, startDateMax, endDateMin, endDateMax, leaveRequestStatus, requestedOnMin, requestedOnMax, reviewedOnMin, reviewedOnMax, workflowInstanceId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? LeaveRequestConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            LeaveRequestType? leaveRequestType = null,
            DateTime? startDateMin = null,
            DateTime? startDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            LeaveRequestStatus? leaveRequestStatus = null,
            DateTime? requestedOnMin = null,
            DateTime? requestedOnMax = null,
            DateTime? reviewedOnMin = null,
            DateTime? reviewedOnMax = null,
            string? workflowInstanceId = null,
            Guid? employeeId = null,
            Guid? reviewedBy = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, leaveRequestType, startDateMin, startDateMax, endDateMin, endDateMax, leaveRequestStatus, requestedOnMin, requestedOnMax, reviewedOnMin, reviewedOnMax, workflowInstanceId, employeeId, reviewedBy);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<LeaveRequest> ApplyFilter(
            IQueryable<LeaveRequest> query,
            string? filterText = null,
            LeaveRequestType? leaveRequestType = null,
            DateTime? startDateMin = null,
            DateTime? startDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            LeaveRequestStatus? leaveRequestStatus = null,
            DateTime? requestedOnMin = null,
            DateTime? requestedOnMax = null,
            DateTime? reviewedOnMin = null,
            DateTime? reviewedOnMax = null,
            string? workflowInstanceId = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.WorkflowInstanceId!.Contains(filterText!))
                    .WhereIf(leaveRequestType.HasValue, e => e.LeaveRequestType == leaveRequestType)
                    .WhereIf(startDateMin.HasValue, e => e.StartDate >= startDateMin!.Value)
                    .WhereIf(startDateMax.HasValue, e => e.StartDate <= startDateMax!.Value)
                    .WhereIf(endDateMin.HasValue, e => e.EndDate >= endDateMin!.Value)
                    .WhereIf(endDateMax.HasValue, e => e.EndDate <= endDateMax!.Value)
                    .WhereIf(leaveRequestStatus.HasValue, e => e.LeaveRequestStatus == leaveRequestStatus)
                    .WhereIf(requestedOnMin.HasValue, e => e.RequestedOn >= requestedOnMin!.Value)
                    .WhereIf(requestedOnMax.HasValue, e => e.RequestedOn <= requestedOnMax!.Value)
                    .WhereIf(reviewedOnMin.HasValue, e => e.ReviewedOn >= reviewedOnMin!.Value)
                    .WhereIf(reviewedOnMax.HasValue, e => e.ReviewedOn <= reviewedOnMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(workflowInstanceId), e => e.WorkflowInstanceId.Contains(workflowInstanceId));
        }
    }
}