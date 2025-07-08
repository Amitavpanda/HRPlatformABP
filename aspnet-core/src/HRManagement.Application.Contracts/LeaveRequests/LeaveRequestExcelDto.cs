using HRManagement;
using System;

namespace HRManagement.LeaveRequests
{
    public abstract class LeaveRequestExcelDtoBase
    {
        public LeaveRequestType LeaveRequestType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveRequestStatus LeaveRequestStatus { get; set; }
        public DateTime RequestedOn { get; set; }
        public DateTime? ReviewedOn { get; set; }
        public string? WorkflowInstanceId { get; set; }
    }
}