using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.PayrollRecords;
using HRManagement.EntityFrameworkCore;
using Xunit;

namespace HRManagement.EntityFrameworkCore.Domains.PayrollRecords
{
    public class PayrollRecordRepositoryTests : HRManagementEntityFrameworkCoreTestBase
    {
        private readonly IPayrollRecordRepository _payrollRecordRepository;

        public PayrollRecordRepositoryTests()
        {
            _payrollRecordRepository = GetRequiredService<IPayrollRecordRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _payrollRecordRepository.GetListAsync(
                    status: default,
                    payslipUrl: "44eddc9ee6004901ab47dda6c61e55eac0e706f4a6a"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("da58d51c-e5f7-4a51-b512-7ba283898446"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _payrollRecordRepository.GetCountAsync(
                    status: default,
                    payslipUrl: "792239ce64c14c9b968e2bc53e38279f8429a4f110424c77a6a5e"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}