using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace HRManagement.Employees
{
    public abstract class EmployeeManagerBase : DomainService
    {
        protected IEmployeeRepository _employeeRepository;

        public EmployeeManagerBase(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public virtual async Task<Employee> CreateAsync(
        Guid? identityUserId, DateTime dateOfJoining, decimal leaveBalance, decimal baseSalary, string? employeeNumber = null)
        {
            Check.NotNull(dateOfJoining, nameof(dateOfJoining));
            Check.Range(leaveBalance, nameof(leaveBalance), EmployeeConsts.LeaveBalanceMinLength, EmployeeConsts.LeaveBalanceMaxLength);
            Check.Length(employeeNumber, nameof(employeeNumber), EmployeeConsts.EmployeeNumberMaxLength, EmployeeConsts.EmployeeNumberMinLength);

            var employee = new Employee(
             GuidGenerator.Create(),
             identityUserId, dateOfJoining, leaveBalance, baseSalary, employeeNumber
             );

            return await _employeeRepository.InsertAsync(employee);
        }

        public virtual async Task<Employee> UpdateAsync(
            Guid id,
            Guid? identityUserId, DateTime dateOfJoining, decimal leaveBalance, decimal baseSalary, string? employeeNumber = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(dateOfJoining, nameof(dateOfJoining));
            Check.Range(leaveBalance, nameof(leaveBalance), EmployeeConsts.LeaveBalanceMinLength, EmployeeConsts.LeaveBalanceMaxLength);
            Check.Length(employeeNumber, nameof(employeeNumber), EmployeeConsts.EmployeeNumberMaxLength, EmployeeConsts.EmployeeNumberMinLength);

            var employee = await _employeeRepository.GetAsync(id);

            employee.IdentityUserId = identityUserId;
            employee.DateOfJoining = dateOfJoining;
            employee.LeaveBalance = leaveBalance;
            employee.BaseSalary = baseSalary;
            employee.EmployeeNumber = employeeNumber;

            employee.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _employeeRepository.UpdateAsync(employee);
        }

    }
}