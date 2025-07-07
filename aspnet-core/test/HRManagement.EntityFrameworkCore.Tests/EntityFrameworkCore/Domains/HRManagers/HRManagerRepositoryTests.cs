using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.HRManagers;
using HRManagement.EntityFrameworkCore;
using Xunit;

namespace HRManagement.EntityFrameworkCore.Domains.HRManagers
{
    public class HRManagerRepositoryTests : HRManagementEntityFrameworkCoreTestBase
    {
        private readonly IHRManagerRepository _hRManagerRepository;

        public HRManagerRepositoryTests()
        {
            _hRManagerRepository = GetRequiredService<IHRManagerRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _hRManagerRepository.GetListAsync(
                    department: "8e51ea166023497dbe19be5bb828d2951a6898dd5ec8439588c1bbccb464243c8448e1d147e941218c946e3ea93601b4a3750151ab70444c86b6e450018bb8a322e370763c2a4ae88048812f37db165b43fb22ab559146ba8f822e013fbf2de85cbe098e",
                    hRNumber: "042d6b884c4540cb91ade66dea3bcd2f37dd19cb98e14639aea0fcedf8bcacbbfebf1d469e4d4f62a42b1f0b91d3072ae70802cc380e4ccb9abac4287505060ee737cec6cfd1466593288aa98686f686da5fce1da60f4f83b511ae2137aac9630eb99b43"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("7aa2ef2d-ab85-4346-92bb-925be5d43511"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _hRManagerRepository.GetCountAsync(
                    department: "36ea6fb31060434399e467049b6596d57775678e48dc424797ef0965afb750fb6b3afb0e406045fb9dacff16e0573303dbf05a1bf7944ef1a68d308bfd465adc859c227b8c194fc99ae7c992a8ca716b041957b670534970bb997db0684cc1302c3c82bd",
                    hRNumber: "637a9d660ec54b1d9243f2f649a15175899b3b8c26364f8a8366380542f0a42efa8b3afb516d4ceaa2ed17b01af975f27a3627e0116e4fbcaa8df0e24d08433d7f1b14820e0a4fb490e0c5798d39ab69cd2e385a0431479bb41297766039efa55347b90e"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}