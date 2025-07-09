using HRManagement.Employees;

using System;
using System.Collections.Generic;

namespace HRManagement.PayrollRecords
{
    public abstract class PayrollRecordWithNavigationPropertiesBase
    {
        public PayrollRecord PayrollRecord { get; set; } = null!;

        public Employee Employee { get; set; } = null!;
        

        
    }
}