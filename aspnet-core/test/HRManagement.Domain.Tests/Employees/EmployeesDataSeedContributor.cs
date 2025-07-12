using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using HRManagement.Employees;

namespace HRManagement.Employees
{
    public class EmployeesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public EmployeesDataSeedContributor(IEmployeeRepository employeeRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _employeeRepository = employeeRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _employeeRepository.InsertAsync(new Employee
            (
                id: Guid.Parse("2ebac9f1-3c46-4110-9ed9-b8a8ae7b4c9d"),
                employeeNumber: "def6f9c66e014679921bfa2ce5aa41b095d2d5ed21524e9a8a7ecf957a719a2a7eed955385cf45f89dc4313fc3d294149d18",
                dateOfJoining: new DateTime(2018, 5, 22),
                paidLeaveBalance: 736,
                baseSalary: 821303484,
                unpaidLeaveBalance: 71,
                sickLeaveBalance: 807,
                deductionPerDay: 542,
                identityUserId: null
            ));

            await _employeeRepository.InsertAsync(new Employee
            (
                id: Guid.Parse("cb6146a5-93f6-44f4-ad74-6a6c2062a103"),
                employeeNumber: "9a4c3f136fa14e469c71c5c998044a1afd79ef2f52a246a7976f88f90c36160cdb0c82728af04f7c8211bb760fe67b677d8d",
                dateOfJoining: new DateTime(2013, 2, 5),
                paidLeaveBalance: 91,
                baseSalary: 1078563056,
                unpaidLeaveBalance: 803,
                sickLeaveBalance: 114,
                deductionPerDay: 799,
                identityUserId: null
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}