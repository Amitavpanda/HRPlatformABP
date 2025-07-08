using HRManagement;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HRManagement.LeaveRequests
{
    public partial interface ILeaveRequestRepository : IRepository<LeaveRequest, Guid>
    {

        Task DeleteAllAsync(
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
            CancellationToken cancellationToken = default);
        Task<LeaveRequestWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );

        Task<List<LeaveRequestWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<LeaveRequest>> GetListAsync(
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
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}