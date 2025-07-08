using HRManagement;
using Volo.Abp.Application.Dtos;
using System;

namespace HRManagement.LeaveRequests
{
    public abstract class LeaveRequestExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public LeaveRequestType? LeaveRequestType { get; set; }
        public DateTime? StartDateMin { get; set; }
        public DateTime? StartDateMax { get; set; }
        public DateTime? EndDateMin { get; set; }
        public DateTime? EndDateMax { get; set; }
        public LeaveRequestStatus? LeaveRequestStatus { get; set; }
        public DateTime? RequestedOnMin { get; set; }
        public DateTime? RequestedOnMax { get; set; }
        public DateTime? ReviewedOnMin { get; set; }
        public DateTime? ReviewedOnMax { get; set; }
        public string? WorkflowInstanceId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? ReviewedBy { get; set; }

        public LeaveRequestExcelDownloadDtoBase()
        {

        }
    }
}