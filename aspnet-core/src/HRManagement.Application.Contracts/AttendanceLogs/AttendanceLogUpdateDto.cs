using HRManagement;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace HRManagement.AttendanceLogs
{
    public abstract class AttendanceLogUpdateDtoBase : IHasConcurrencyStamp
    {
        public DateTime Date { get; set; }
        public TimeOnly CheckInTime { get; set; }
        public TimeOnly CheckOutTime { get; set; }
        public AttendanceStatus Status { get; set; }
        public Guid EmployeeId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}