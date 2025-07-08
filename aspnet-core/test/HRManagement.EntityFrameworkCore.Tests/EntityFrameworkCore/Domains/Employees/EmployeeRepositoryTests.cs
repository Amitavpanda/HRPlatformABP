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
                    employeeNumber: "77bbf33250304481a8dee3b99b80cb480f728e16f58c46d496de96a2f9f221859e1eab6b9fb840808bf2f587f1cc1ce987b7"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc"));
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
                    employeeNumber: "78f849fd599d45d5841f222d049470c43007e3c9dd7a49a8875989aadfdfc3865dd9aee7c6544deca97f1947fd13e23084a6"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}