using HRManagement;
using System;

namespace HRManagement.AttendanceLogs
{
    public abstract class AttendanceLogExcelDtoBase
    {
        public DateTime Date { get; set; }
        public TimeOnly CheckInTime { get; set; }
        public TimeOnly CheckOutTime { get; set; }
        public AttendanceStatus Status { get; set; }
    }
}