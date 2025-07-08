using HRManagement;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HRManagement.LeaveRequests
{
    public abstract class LeaveRequestCreateDtoBase
    {
        public LeaveRequestType LeaveRequestType { get; set; } = ((LeaveRequestType[])Enum.GetValues(typeof(LeaveRequestType)))[0];
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveRequestStatus LeaveRequestStatus { get; set; } = ((LeaveRequestStatus[])Enum.GetValues(typeof(LeaveRequestStatus)))[0];
        public DateTime RequestedOn { get; set; }
        public DateTime? ReviewedOn { get; set; }
        [StringLength(LeaveRequestConsts.WorkflowInstanceIdMaxLength, MinimumLength = LeaveRequestConsts.WorkflowInstanceIdMinLength)]
        public string? WorkflowInstanceId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid? ReviewedBy { get; set; }
    }
}