using HRManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace HRManagement.AttendanceLogs
{
    public abstract class AttendanceLogManagerBase : DomainService
    {
        protected IAttendanceLogRepository _attendanceLogRepository;

        public AttendanceLogManagerBase(IAttendanceLogRepository attendanceLogRepository)
        {
            _attendanceLogRepository = attendanceLogRepository;
        }

        public virtual async Task<AttendanceLog> CreateAsync(
        Guid employeeId, DateTime date, TimeOnly checkInTime, TimeOnly checkOutTime, AttendanceStatus status)
        {
            Check.NotNull(employeeId, nameof(employeeId));
            Check.NotNull(date, nameof(date));
            Check.NotNull(status, nameof(status));

            var attendanceLog = new AttendanceLog(
             GuidGenerator.Create(),
             employeeId, date, checkInTime, checkOutTime, status
             );

            return await _attendanceLogRepository.InsertAsync(attendanceLog);
        }

        public virtual async Task<AttendanceLog> UpdateAsync(
            Guid id,
            Guid employeeId, DateTime date, TimeOnly checkInTime, TimeOnly checkOutTime, AttendanceStatus status, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(employeeId, nameof(employeeId));
            Check.NotNull(date, nameof(date));
            Check.NotNull(status, nameof(status));

            var attendanceLog = await _attendanceLogRepository.GetAsync(id);

            attendanceLog.EmployeeId = employeeId;
            attendanceLog.Date = date;
            attendanceLog.CheckInTime = checkInTime;
            attendanceLog.CheckOutTime = checkOutTime;
            attendanceLog.Status = status;

            attendanceLog.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _attendanceLogRepository.UpdateAsync(attendanceLog);
        }

    }
}