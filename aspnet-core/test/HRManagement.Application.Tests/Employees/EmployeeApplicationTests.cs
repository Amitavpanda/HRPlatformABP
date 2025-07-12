using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace HRManagement.Employees
{
    public abstract class EmployeesAppServiceTests<TStartupModule> : HRManagementApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IEmployeesAppService _employeesAppService;
        private readonly IRepository<Employee, Guid> _employeeRepository;

        public EmployeesAppServiceTests()
        {
            _employeesAppService = GetRequiredService<IEmployeesAppService>();
            _employeeRepository = GetRequiredService<IRepository<Employee, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _employeesAppService.GetListAsync(new GetEmployeesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Employee.Id == Guid.Parse("2ebac9f1-3c46-4110-9ed9-b8a8ae7b4c9d")).ShouldBe(true);
            result.Items.Any(x => x.Employee.Id == Guid.Parse("cb6146a5-93f6-44f4-ad74-6a6c2062a103")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _employeesAppService.GetAsync(Guid.Parse("2ebac9f1-3c46-4110-9ed9-b8a8ae7b4c9d"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("2ebac9f1-3c46-4110-9ed9-b8a8ae7b4c9d"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EmployeeCreateDto
            {
                EmployeeNumber = "701901d1f6f346ec86c068e6bd1f67be27604909ac894e2fa3ccbddfc64d161e7ea66abd8e144a129d30fb4b30e330b8e91d",
                DateOfJoining = new DateTime(2013, 10, 18),
                PaidLeaveBalance = 133,
                BaseSalary = 814527125,
                UnpaidLeaveBalance = 678,
                SickLeaveBalance = 371,
                DeductionPerDay = 302
            };

            // Act
            var serviceResult = await _employeesAppService.CreateAsync(input);

            // Assert
            var result = await _employeeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EmployeeNumber.ShouldBe("701901d1f6f346ec86c068e6bd1f67be27604909ac894e2fa3ccbddfc64d161e7ea66abd8e144a129d30fb4b30e330b8e91d");
            result.DateOfJoining.ShouldBe(new DateTime(2013, 10, 18));
            result.PaidLeaveBalance.ShouldBe(133);
            result.BaseSalary.ShouldBe(814527125);
            result.UnpaidLeaveBalance.ShouldBe(678);
            result.SickLeaveBalance.ShouldBe(371);
            result.DeductionPerDay.ShouldBe(302);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EmployeeUpdateDto()
            {
                EmployeeNumber = "0ce56c33918e457ba89aa5b143f8f07b9dd3563a311d44b0b57b767a8f05447340c184cb21df4399aebac3dcfc8ddca7cc9d",
                DateOfJoining = new DateTime(2018, 4, 17),
                PaidLeaveBalance = 73,
                BaseSalary = 998417846,
                UnpaidLeaveBalance = 324,
                SickLeaveBalance = 228,
                DeductionPerDay = 316
            };

            // Act
            var serviceResult = await _employeesAppService.UpdateAsync(Guid.Parse("2ebac9f1-3c46-4110-9ed9-b8a8ae7b4c9d"), input);

            // Assert
            var result = await _employeeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EmployeeNumber.ShouldBe("0ce56c33918e457ba89aa5b143f8f07b9dd3563a311d44b0b57b767a8f05447340c184cb21df4399aebac3dcfc8ddca7cc9d");
            result.DateOfJoining.ShouldBe(new DateTime(2018, 4, 17));
            result.PaidLeaveBalance.ShouldBe(73);
            result.BaseSalary.ShouldBe(998417846);
            result.UnpaidLeaveBalance.ShouldBe(324);
            result.SickLeaveBalance.ShouldBe(228);
            result.DeductionPerDay.ShouldBe(316);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _employeesAppService.DeleteAsync(Guid.Parse("2ebac9f1-3c46-4110-9ed9-b8a8ae7b4c9d"));

            // Assert
            var result = await _employeeRepository.FindAsync(c => c.Id == Guid.Parse("2ebac9f1-3c46-4110-9ed9-b8a8ae7b4c9d"));

            result.ShouldBeNull();
        }
    }
}