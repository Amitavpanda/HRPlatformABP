using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace HRManagement.PayrollRecords
{
    public abstract class PayrollRecordsAppServiceTests<TStartupModule> : HRManagementApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IPayrollRecordsAppService _payrollRecordsAppService;
        private readonly IRepository<PayrollRecord, Guid> _payrollRecordRepository;

        public PayrollRecordsAppServiceTests()
        {
            _payrollRecordsAppService = GetRequiredService<IPayrollRecordsAppService>();
            _payrollRecordRepository = GetRequiredService<IRepository<PayrollRecord, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _payrollRecordsAppService.GetListAsync(new GetPayrollRecordsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.PayrollRecord.Id == Guid.Parse("da58d51c-e5f7-4a51-b512-7ba283898446")).ShouldBe(true);
            result.Items.Any(x => x.PayrollRecord.Id == Guid.Parse("0a21286c-3f18-4271-91f7-401c11f19fed")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _payrollRecordsAppService.GetAsync(Guid.Parse("da58d51c-e5f7-4a51-b512-7ba283898446"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("da58d51c-e5f7-4a51-b512-7ba283898446"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PayrollRecordCreateDto
            {
                Month = 0,
                Year = 1371,
                BaseSalary = 1625165983,
                LeaveDeductions = 1373931370,
                NetPay = 423579517,
                Status = default,
                PayslipUrl = "d27ff116a8ae49d9958d59acb1f83244faa44",
                EmployeeId = Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc")
            };

            // Act
            var serviceResult = await _payrollRecordsAppService.CreateAsync(input);

            // Assert
            var result = await _payrollRecordRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Month.ShouldBe(0);
            result.Year.ShouldBe(1371);
            result.BaseSalary.ShouldBe(1625165983);
            result.LeaveDeductions.ShouldBe(1373931370);
            result.NetPay.ShouldBe(423579517);
            result.Status.ShouldBe(default);
            result.PayslipUrl.ShouldBe("d27ff116a8ae49d9958d59acb1f83244faa44");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PayrollRecordUpdateDto()
            {
                Month = 0,
                Year = 9939,
                BaseSalary = 396306702,
                LeaveDeductions = 1281069456,
                NetPay = 159945235,
                Status = default,
                PayslipUrl = "8801bf2ade4f4156a666eec13ce662f571833aa5e8394044a60663f9cde23c90ca4356",
                EmployeeId = Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc")
            };

            // Act
            var serviceResult = await _payrollRecordsAppService.UpdateAsync(Guid.Parse("da58d51c-e5f7-4a51-b512-7ba283898446"), input);

            // Assert
            var result = await _payrollRecordRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Month.ShouldBe(0);
            result.Year.ShouldBe(9939);
            result.BaseSalary.ShouldBe(396306702);
            result.LeaveDeductions.ShouldBe(1281069456);
            result.NetPay.ShouldBe(159945235);
            result.Status.ShouldBe(default);
            result.PayslipUrl.ShouldBe("8801bf2ade4f4156a666eec13ce662f571833aa5e8394044a60663f9cde23c90ca4356");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _payrollRecordsAppService.DeleteAsync(Guid.Parse("da58d51c-e5f7-4a51-b512-7ba283898446"));

            // Assert
            var result = await _payrollRecordRepository.FindAsync(c => c.Id == Guid.Parse("da58d51c-e5f7-4a51-b512-7ba283898446"));

            result.ShouldBeNull();
        }
    }
}