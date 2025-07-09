using HRManagement.Employees;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace HRManagement.PayrollRecords
{
    public abstract class PayrollRecordWithNavigationPropertiesDtoBase
    {
        public PayrollRecordDto PayrollRecord { get; set; } = null!;

        public EmployeeDto Employee { get; set; } = null!;

    }
}