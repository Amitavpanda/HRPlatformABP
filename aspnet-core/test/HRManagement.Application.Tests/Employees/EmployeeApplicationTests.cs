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
            result.Items.Any(x => x.Employee.Id == Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc")).ShouldBe(true);
            result.Items.Any(x => x.Employee.Id == Guid.Parse("e0ec921f-6948-47f7-ac3b-9bab00eb724f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _employeesAppService.GetAsync(Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EmployeeCreateDto
            {
                EmployeeNumber = "17b836b8af4944a9bfc2af5b4fa5a9980a6d5d0fa90b40baad1bf95aaf8ae5d0415ef0b0707146d3a1c5052b474ae2171656",
                DateOfJoining = new DateTime(2016, 5, 24),
                LeaveBalance = 671,
                BaseSalary = 922404883
            };

            // Act
            var serviceResult = await _employeesAppService.CreateAsync(input);

            // Assert
            var result = await _employeeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EmployeeNumber.ShouldBe("17b836b8af4944a9bfc2af5b4fa5a9980a6d5d0fa90b40baad1bf95aaf8ae5d0415ef0b0707146d3a1c5052b474ae2171656");
            result.DateOfJoining.ShouldBe(new DateTime(2016, 5, 24));
            result.LeaveBalance.ShouldBe(671);
            result.BaseSalary.ShouldBe(922404883);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EmployeeUpdateDto()
            {
                EmployeeNumber = "d91e7cebac94471197d92918335c92079fedbad99dad4254ba40cd38592bc9ab511b39478f0e4a0bb8cb8347bc8135c9f129",
                DateOfJoining = new DateTime(2014, 11, 5),
                LeaveBalance = 231,
                BaseSalary = 1018314447
            };

            // Act
            var serviceResult = await _employeesAppService.UpdateAsync(Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc"), input);

            // Assert
            var result = await _employeeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EmployeeNumber.ShouldBe("d91e7cebac94471197d92918335c92079fedbad99dad4254ba40cd38592bc9ab511b39478f0e4a0bb8cb8347bc8135c9f129");
            result.DateOfJoining.ShouldBe(new DateTime(2014, 11, 5));
            result.LeaveBalance.ShouldBe(231);
            result.BaseSalary.ShouldBe(1018314447);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _employeesAppService.DeleteAsync(Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc"));

            // Assert
            var result = await _employeeRepository.FindAsync(c => c.Id == Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc"));

            result.ShouldBeNull();
        }
    }
}