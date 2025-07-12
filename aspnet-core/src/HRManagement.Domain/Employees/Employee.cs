using Volo.Abp.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace HRManagement.Employees
{
    public abstract class EmployeeBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? EmployeeNumber { get; set; }

        public virtual DateTime DateOfJoining { get; set; }

        public virtual decimal PaidLeaveBalance { get; set; }

        public virtual decimal BaseSalary { get; set; }

        public virtual decimal UnpaidLeaveBalance { get; set; }

        public virtual decimal SickLeaveBalance { get; set; }

        public virtual decimal DeductionPerDay { get; set; }
        public Guid? IdentityUserId { get; set; }

        protected EmployeeBase()
        {

        }

        public EmployeeBase(Guid id, Guid? identityUserId, DateTime dateOfJoining, decimal paidLeaveBalance, decimal baseSalary, decimal unpaidLeaveBalance, decimal sickLeaveBalance, decimal deductionPerDay, string? employeeNumber = null)
        {

            Id = id;
            if (paidLeaveBalance < EmployeeConsts.PaidLeaveBalanceMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(paidLeaveBalance), paidLeaveBalance, "The value of 'paidLeaveBalance' cannot be lower than " + EmployeeConsts.PaidLeaveBalanceMinLength);
            }

            if (paidLeaveBalance > EmployeeConsts.PaidLeaveBalanceMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(paidLeaveBalance), paidLeaveBalance, "The value of 'paidLeaveBalance' cannot be greater than " + EmployeeConsts.PaidLeaveBalanceMaxLength);
            }

            if (unpaidLeaveBalance < EmployeeConsts.UnpaidLeaveBalanceMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(unpaidLeaveBalance), unpaidLeaveBalance, "The value of 'unpaidLeaveBalance' cannot be lower than " + EmployeeConsts.UnpaidLeaveBalanceMinLength);
            }

            if (unpaidLeaveBalance > EmployeeConsts.UnpaidLeaveBalanceMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(unpaidLeaveBalance), unpaidLeaveBalance, "The value of 'unpaidLeaveBalance' cannot be greater than " + EmployeeConsts.UnpaidLeaveBalanceMaxLength);
            }

            if (sickLeaveBalance < EmployeeConsts.SickLeaveBalanceMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(sickLeaveBalance), sickLeaveBalance, "The value of 'sickLeaveBalance' cannot be lower than " + EmployeeConsts.SickLeaveBalanceMinLength);
            }

            if (sickLeaveBalance > EmployeeConsts.SickLeaveBalanceMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(sickLeaveBalance), sickLeaveBalance, "The value of 'sickLeaveBalance' cannot be greater than " + EmployeeConsts.SickLeaveBalanceMaxLength);
            }

            if (deductionPerDay < EmployeeConsts.DeductionPerDayMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(deductionPerDay), deductionPerDay, "The value of 'deductionPerDay' cannot be lower than " + EmployeeConsts.DeductionPerDayMinLength);
            }

            if (deductionPerDay > EmployeeConsts.DeductionPerDayMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(deductionPerDay), deductionPerDay, "The value of 'deductionPerDay' cannot be greater than " + EmployeeConsts.DeductionPerDayMaxLength);
            }

            Check.Length(employeeNumber, nameof(employeeNumber), EmployeeConsts.EmployeeNumberMaxLength, EmployeeConsts.EmployeeNumberMinLength);
            DateOfJoining = dateOfJoining;
            PaidLeaveBalance = paidLeaveBalance;
            BaseSalary = baseSalary;
            UnpaidLeaveBalance = unpaidLeaveBalance;
            SickLeaveBalance = sickLeaveBalance;
            DeductionPerDay = deductionPerDay;
            EmployeeNumber = employeeNumber;
            IdentityUserId = identityUserId;
        }

    }
}