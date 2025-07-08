using HRManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace HRManagement.LeaveRequests
{
    public abstract class LeaveRequestManagerBase : DomainService
    {
        protected ILeaveRequestRepository _leaveRequestRepository;

        public LeaveRequestManagerBase(ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }

        public virtual async Task<LeaveRequest> CreateAsync(
        Guid employeeId, Guid? reviewedBy, LeaveRequestType leaveRequestType, DateTime startDate, DateTime endDate, LeaveRequestStatus leaveRequestStatus, DateTime requestedOn, DateTime? reviewedOn = null, string? workflowInstanceId = null)
        {
            Check.NotNull(employeeId, nameof(employeeId));
            Check.NotNull(leaveRequestType, nameof(leaveRequestType));
            Check.NotNull(startDate, nameof(startDate));
            Check.NotNull(endDate, nameof(endDate));
            Check.NotNull(leaveRequestStatus, nameof(leaveRequestStatus));
            Check.NotNull(requestedOn, nameof(requestedOn));
            Check.Length(workflowInstanceId, nameof(workflowInstanceId), LeaveRequestConsts.WorkflowInstanceIdMaxLength, LeaveRequestConsts.WorkflowInstanceIdMinLength);

            var leaveRequest = new LeaveRequest(
             GuidGenerator.Create(),
             employeeId, reviewedBy, leaveRequestType, startDate, endDate, leaveRequestStatus, requestedOn, reviewedOn, workflowInstanceId
             );

            return await _leaveRequestRepository.InsertAsync(leaveRequest);
        }

        public virtual async Task<LeaveRequest> UpdateAsync(
            Guid id,
            Guid employeeId, Guid? reviewedBy, LeaveRequestType leaveRequestType, DateTime startDate, DateTime endDate, LeaveRequestStatus leaveRequestStatus, DateTime requestedOn, DateTime? reviewedOn = null, string? workflowInstanceId = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(employeeId, nameof(employeeId));
            Check.NotNull(leaveRequestType, nameof(leaveRequestType));
            Check.NotNull(startDate, nameof(startDate));
            Check.NotNull(endDate, nameof(endDate));
            Check.NotNull(leaveRequestStatus, nameof(leaveRequestStatus));
            Check.NotNull(requestedOn, nameof(requestedOn));
            Check.Length(workflowInstanceId, nameof(workflowInstanceId), LeaveRequestConsts.WorkflowInstanceIdMaxLength, LeaveRequestConsts.WorkflowInstanceIdMinLength);

            var leaveRequest = await _leaveRequestRepository.GetAsync(id);

            leaveRequest.EmployeeId = employeeId;
            leaveRequest.ReviewedBy = reviewedBy;
            leaveRequest.LeaveRequestType = leaveRequestType;
            leaveRequest.StartDate = startDate;
            leaveRequest.EndDate = endDate;
            leaveRequest.LeaveRequestStatus = leaveRequestStatus;
            leaveRequest.RequestedOn = requestedOn;
            leaveRequest.ReviewedOn = reviewedOn;
            leaveRequest.WorkflowInstanceId = workflowInstanceId;

            leaveRequest.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _leaveRequestRepository.UpdateAsync(leaveRequest);
        }

    }
}