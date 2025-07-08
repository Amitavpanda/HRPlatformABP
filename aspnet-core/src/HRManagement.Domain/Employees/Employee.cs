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

        public virtual decimal LeaveBalance { get; set; }

        public virtual decimal BaseSalary { get; set; }
        public Guid? IdentityUserId { get; set; }

        protected EmployeeBase()
        {

        }

        public EmployeeBase(Guid id, Guid? identityUserId, DateTime dateOfJoining, decimal leaveBalance, decimal baseSalary, string? employeeNumber = null)
        {

            Id = id;
            if (leaveBalance < EmployeeConsts.LeaveBalanceMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(leaveBalance), leaveBalance, "The value of 'leaveBalance' cannot be lower than " + EmployeeConsts.LeaveBalanceMinLength);
            }

            if (leaveBalance > EmployeeConsts.LeaveBalanceMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(leaveBalance), leaveBalance, "The value of 'leaveBalance' cannot be greater than " + EmployeeConsts.LeaveBalanceMaxLength);
            }

            Check.Length(employeeNumber, nameof(employeeNumber), EmployeeConsts.EmployeeNumberMaxLength, EmployeeConsts.EmployeeNumberMinLength);
            DateOfJoining = dateOfJoining;
            LeaveBalance = leaveBalance;
            BaseSalary = baseSalary;
            EmployeeNumber = employeeNumber;
            IdentityUserId = identityUserId;
        }

    }
}