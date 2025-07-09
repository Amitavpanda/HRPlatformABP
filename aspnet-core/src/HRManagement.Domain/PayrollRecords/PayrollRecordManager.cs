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

namespace HRManagement.PayrollRecords
{
    public abstract class PayrollRecordManagerBase : DomainService
    {
        protected IPayrollRecordRepository _payrollRecordRepository;

        public PayrollRecordManagerBase(IPayrollRecordRepository payrollRecordRepository)
        {
            _payrollRecordRepository = payrollRecordRepository;
        }

        public virtual async Task<PayrollRecord> CreateAsync(
        Guid employeeId, int month, int year, decimal baseSalary, decimal leaveDeductions, decimal netPay, PayrollRecordStatus status, string? payslipUrl = null)
        {
            Check.NotNull(employeeId, nameof(employeeId));
            Check.Range(month, nameof(month), PayrollRecordConsts.MonthMinLength, PayrollRecordConsts.MonthMaxLength);
            Check.Range(year, nameof(year), PayrollRecordConsts.YearMinLength, PayrollRecordConsts.YearMaxLength);
            Check.NotNull(status, nameof(status));

            var payrollRecord = new PayrollRecord(
             GuidGenerator.Create(),
             employeeId, month, year, baseSalary, leaveDeductions, netPay, status, payslipUrl
             );

            return await _payrollRecordRepository.InsertAsync(payrollRecord);
        }

        public virtual async Task<PayrollRecord> UpdateAsync(
            Guid id,
            Guid employeeId, int month, int year, decimal baseSalary, decimal leaveDeductions, decimal netPay, PayrollRecordStatus status, string? payslipUrl = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(employeeId, nameof(employeeId));
            Check.Range(month, nameof(month), PayrollRecordConsts.MonthMinLength, PayrollRecordConsts.MonthMaxLength);
            Check.Range(year, nameof(year), PayrollRecordConsts.YearMinLength, PayrollRecordConsts.YearMaxLength);
            Check.NotNull(status, nameof(status));

            var payrollRecord = await _payrollRecordRepository.GetAsync(id);

            payrollRecord.EmployeeId = employeeId;
            payrollRecord.Month = month;
            payrollRecord.Year = year;
            payrollRecord.BaseSalary = baseSalary;
            payrollRecord.LeaveDeductions = leaveDeductions;
            payrollRecord.NetPay = netPay;
            payrollRecord.Status = status;
            payrollRecord.PayslipUrl = payslipUrl;

            payrollRecord.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _payrollRecordRepository.UpdateAsync(payrollRecord);
        }

    }
}