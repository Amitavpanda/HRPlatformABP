using HRManagement;
using HRManagement.Employees;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace HRManagement.AttendanceLogs
{
    public abstract class AttendanceLogBase : FullAuditedAggregateRoot<Guid>
    {
        public virtual DateTime Date { get; set; }

        public virtual TimeOnly CheckInTime { get; set; }

        public virtual TimeOnly CheckOutTime { get; set; }

        public virtual AttendanceStatus Status { get; set; }
        public Guid EmployeeId { get; set; }

        protected AttendanceLogBase()
        {

        }

        public AttendanceLogBase(Guid id, Guid employeeId, DateTime date, TimeOnly checkInTime, TimeOnly checkOutTime, AttendanceStatus status)
        {

            Id = id;
            Date = date;
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
            Status = status;
            EmployeeId = employeeId;
        }

    }
}