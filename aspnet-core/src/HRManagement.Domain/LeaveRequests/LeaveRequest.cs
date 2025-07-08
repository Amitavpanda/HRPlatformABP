using HRManagement;
using HRManagement.Employees;
using Volo.Abp.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace HRManagement.LeaveRequests
{
    public abstract class LeaveRequestBase : FullAuditedAggregateRoot<Guid>
    {
        public virtual LeaveRequestType LeaveRequestType { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        public virtual LeaveRequestStatus LeaveRequestStatus { get; set; }

        public virtual DateTime RequestedOn { get; set; }

        public virtual DateTime? ReviewedOn { get; set; }

        [CanBeNull]
        public virtual string? WorkflowInstanceId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid? ReviewedBy { get; set; }

        protected LeaveRequestBase()
        {

        }

        public LeaveRequestBase(Guid id, Guid employeeId, Guid? reviewedBy, LeaveRequestType leaveRequestType, DateTime startDate, DateTime endDate, LeaveRequestStatus leaveRequestStatus, DateTime requestedOn, DateTime? reviewedOn = null, string? workflowInstanceId = null)
        {

            Id = id;
            Check.Length(workflowInstanceId, nameof(workflowInstanceId), LeaveRequestConsts.WorkflowInstanceIdMaxLength, LeaveRequestConsts.WorkflowInstanceIdMinLength);
            LeaveRequestType = leaveRequestType;
            StartDate = startDate;
            EndDate = endDate;
            LeaveRequestStatus = leaveRequestStatus;
            RequestedOn = requestedOn;
            ReviewedOn = reviewedOn;
            WorkflowInstanceId = workflowInstanceId;
            EmployeeId = employeeId;
            ReviewedBy = reviewedBy;
        }

    }
}