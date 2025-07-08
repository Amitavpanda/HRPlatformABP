using HRManagement.Employees;

using System;
using System.Collections.Generic;

namespace HRManagement.AttendanceLogs
{
    public abstract class AttendanceLogWithNavigationPropertiesBase
    {
        public AttendanceLog AttendanceLog { get; set; } = null!;

        public Employee Employee { get; set; } = null!;
        

        
    }
}