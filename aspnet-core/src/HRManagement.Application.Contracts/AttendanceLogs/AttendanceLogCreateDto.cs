using HRManagement;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HRManagement.AttendanceLogs
{
    public abstract class AttendanceLogCreateDtoBase
    {
        public DateTime Date { get; set; }
        public TimeOnly CheckInTime { get; set; }
        public TimeOnly CheckOutTime { get; set; }
        public AttendanceStatus Status { get; set; } = ((AttendanceStatus[])Enum.GetValues(typeof(AttendanceStatus)))[0];
        public Guid EmployeeId { get; set; }
    }
}