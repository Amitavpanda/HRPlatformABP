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

namespace HRManagement.PayrollRecords
{
    public abstract class PayrollRecordBase : FullAuditedAggregateRoot<Guid>
    {
        public virtual int Month { get; set; }

        public virtual int Year { get; set; }

        public virtual decimal BaseSalary { get; set; }

        public virtual decimal LeaveDeductions { get; set; }

        public virtual decimal NetPay { get; set; }

        public virtual PayrollRecordStatus Status { get; set; }

        [CanBeNull]
        public virtual string? PayslipUrl { get; set; }
        public Guid EmployeeId { get; set; }

        protected PayrollRecordBase()
        {

        }

        public PayrollRecordBase(Guid id, Guid employeeId, int month, int year, decimal baseSalary, decimal leaveDeductions, decimal netPay, PayrollRecordStatus status, string? payslipUrl = null)
        {

            Id = id;
            if (month < PayrollRecordConsts.MonthMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(month), month, "The value of 'month' cannot be lower than " + PayrollRecordConsts.MonthMinLength);
            }

            if (month > PayrollRecordConsts.MonthMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(month), month, "The value of 'month' cannot be greater than " + PayrollRecordConsts.MonthMaxLength);
            }

            if (year < PayrollRecordConsts.YearMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(year), year, "The value of 'year' cannot be lower than " + PayrollRecordConsts.YearMinLength);
            }

            if (year > PayrollRecordConsts.YearMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(year), year, "The value of 'year' cannot be greater than " + PayrollRecordConsts.YearMaxLength);
            }

            Month = month;
            Year = year;
            BaseSalary = baseSalary;
            LeaveDeductions = leaveDeductions;
            NetPay = netPay;
            Status = status;
            PayslipUrl = payslipUrl;
            EmployeeId = employeeId;
        }

    }
}