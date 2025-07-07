using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace HRManagement.HRManagers
{
    public abstract class HRManagersAppServiceTests<TStartupModule> : HRManagementApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IHRManagersAppService _hRManagersAppService;
        private readonly IRepository<HRManager, Guid> _hRManagerRepository;

        public HRManagersAppServiceTests()
        {
            _hRManagersAppService = GetRequiredService<IHRManagersAppService>();
            _hRManagerRepository = GetRequiredService<IRepository<HRManager, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _hRManagersAppService.GetListAsync(new GetHRManagersInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.HRManager.Id == Guid.Parse("7aa2ef2d-ab85-4346-92bb-925be5d43511")).ShouldBe(true);
            result.Items.Any(x => x.HRManager.Id == Guid.Parse("7cb4dca7-c9ec-4c6b-b0a1-e85c1cfb0804")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _hRManagersAppService.GetAsync(Guid.Parse("7aa2ef2d-ab85-4346-92bb-925be5d43511"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("7aa2ef2d-ab85-4346-92bb-925be5d43511"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new HRManagerCreateDto
            {
                Department = "723d2c20a01c4a679a41d5344c0cced2c0cf1a4969704630bfe284ec0663fd0789888137250f4e1d8fc24c1b117e0189268afcb43be94aaea746a0839dadac85e7b50ba8f710497fbeeb2dfcfd34923cb854e7ec605348a58b60fae97d2e62112fd6f216",
                HRNumber = "781d92de759c4827aac66a6cd1f8ef20c272ad7cc14e4a5e85a42f6a1c0dc58f9d00f78d691f4cd7a7b66133d945c12356b23045f1684c36a48205576361280fccf86ffe3cfa491295908aae1693ed442d574b188b67402aaa4d8aef9be4a061aa230369"
            };

            // Act
            var serviceResult = await _hRManagersAppService.CreateAsync(input);

            // Assert
            var result = await _hRManagerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Department.ShouldBe("723d2c20a01c4a679a41d5344c0cced2c0cf1a4969704630bfe284ec0663fd0789888137250f4e1d8fc24c1b117e0189268afcb43be94aaea746a0839dadac85e7b50ba8f710497fbeeb2dfcfd34923cb854e7ec605348a58b60fae97d2e62112fd6f216");
            result.HRNumber.ShouldBe("781d92de759c4827aac66a6cd1f8ef20c272ad7cc14e4a5e85a42f6a1c0dc58f9d00f78d691f4cd7a7b66133d945c12356b23045f1684c36a48205576361280fccf86ffe3cfa491295908aae1693ed442d574b188b67402aaa4d8aef9be4a061aa230369");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new HRManagerUpdateDto()
            {
                Department = "c5cc580ab2954d079fc15de44c3289cf6c63471122d845118a85ed4e0790c0e28d4a54f29c744b2aa7eede4ceb4ef93774991cffd3f54690a7958a93a3daa7c411ce8ab7fdcd4f1199221c20c9174014bccdd1ff4c16481bb05dd383bab4c4350a396d3c",
                HRNumber = "237470c7dae64456835ca8bfc1dd0907de419f3a3385499a8a9eada1660afb6ddb162db743cb4c7ca664b3969bc8a0c7fc83af7b0029441b8f2426057ec54c7eecb7c02f2a154ae4895b83c9a782515811189a4370994ef2a82c1818f7f226bbd6a4d1e2"
            };

            // Act
            var serviceResult = await _hRManagersAppService.UpdateAsync(Guid.Parse("7aa2ef2d-ab85-4346-92bb-925be5d43511"), input);

            // Assert
            var result = await _hRManagerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Department.ShouldBe("c5cc580ab2954d079fc15de44c3289cf6c63471122d845118a85ed4e0790c0e28d4a54f29c744b2aa7eede4ceb4ef93774991cffd3f54690a7958a93a3daa7c411ce8ab7fdcd4f1199221c20c9174014bccdd1ff4c16481bb05dd383bab4c4350a396d3c");
            result.HRNumber.ShouldBe("237470c7dae64456835ca8bfc1dd0907de419f3a3385499a8a9eada1660afb6ddb162db743cb4c7ca664b3969bc8a0c7fc83af7b0029441b8f2426057ec54c7eecb7c02f2a154ae4895b83c9a782515811189a4370994ef2a82c1818f7f226bbd6a4d1e2");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _hRManagersAppService.DeleteAsync(Guid.Parse("7aa2ef2d-ab85-4346-92bb-925be5d43511"));

            // Assert
            var result = await _hRManagerRepository.FindAsync(c => c.Id == Guid.Parse("7aa2ef2d-ab85-4346-92bb-925be5d43511"));

            result.ShouldBeNull();
        }
    }
}