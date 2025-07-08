using HRManagement;
using Volo.Abp.Application.Dtos;
using System;

namespace HRManagement.AttendanceLogs
{
    public abstract class AttendanceLogExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public DateTime? DateMin { get; set; }
        public DateTime? DateMax { get; set; }
        public TimeOnly? CheckInTimeMin { get; set; }
        public TimeOnly? CheckInTimeMax { get; set; }
        public TimeOnly? CheckOutTimeMin { get; set; }
        public TimeOnly? CheckOutTimeMax { get; set; }
        public AttendanceStatus? Status { get; set; }
        public Guid? EmployeeId { get; set; }

        public AttendanceLogExcelDownloadDtoBase()
        {

        }
    }
}