using HRManagement;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace HRManagement.LeaveRequests
{
    public abstract class LeaveRequestUpdateDtoBase : IHasConcurrencyStamp
    {
        public LeaveRequestType LeaveRequestType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveRequestStatus LeaveRequestStatus { get; set; }
        public DateTime RequestedOn { get; set; }
        public DateTime? ReviewedOn { get; set; }
        [StringLength(LeaveRequestConsts.WorkflowInstanceIdMaxLength, MinimumLength = LeaveRequestConsts.WorkflowInstanceIdMinLength)]
        public string? WorkflowInstanceId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid? ReviewedBy { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}