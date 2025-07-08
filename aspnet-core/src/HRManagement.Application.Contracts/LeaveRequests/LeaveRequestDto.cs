using HRManagement;
using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace HRManagement.LeaveRequests
{
    public abstract class LeaveRequestDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public LeaveRequestType LeaveRequestType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveRequestStatus LeaveRequestStatus { get; set; }
        public DateTime RequestedOn { get; set; }
        public DateTime? ReviewedOn { get; set; }
        public string? WorkflowInstanceId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid? ReviewedBy { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}