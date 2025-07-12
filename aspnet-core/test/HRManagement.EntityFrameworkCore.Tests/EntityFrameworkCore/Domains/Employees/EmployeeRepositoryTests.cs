using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.Employees;
using HRManagement.EntityFrameworkCore;
using Xunit;

namespace HRManagement.EntityFrameworkCore.Domains.Employees
{
    public class EmployeeRepositoryTests : HRManagementEntityFrameworkCoreTestBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeRepositoryTests()
        {
            _employeeRepository = GetRequiredService<IEmployeeRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _employeeRepository.GetListAsync(
                    employeeNumber: "def6f9c66e014679921bfa2ce5aa41b095d2d5ed21524e9a8a7ecf957a719a2a7eed955385cf45f89dc4313fc3d294149d18"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("2ebac9f1-3c46-4110-9ed9-b8a8ae7b4c9d"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _employeeRepository.GetCountAsync(
                    employeeNumber: "9a4c3f136fa14e469c71c5c998044a1afd79ef2f52a246a7976f88f90c36160cdb0c82728af04f7c8211bb760fe67b677d8d"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}