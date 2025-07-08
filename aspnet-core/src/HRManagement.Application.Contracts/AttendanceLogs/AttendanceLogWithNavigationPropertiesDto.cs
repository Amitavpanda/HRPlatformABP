using HRManagement.Employees;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace HRManagement.AttendanceLogs
{
    public abstract class AttendanceLogWithNavigationPropertiesDtoBase
    {
        public AttendanceLogDto AttendanceLog { get; set; } = null!;

        public EmployeeDto Employee { get; set; } = null!;

    }
}