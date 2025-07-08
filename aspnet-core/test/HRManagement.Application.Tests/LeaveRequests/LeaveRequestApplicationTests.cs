using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace HRManagement.LeaveRequests
{
    public abstract class LeaveRequestsAppServiceTests<TStartupModule> : HRManagementApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly ILeaveRequestsAppService _leaveRequestsAppService;
        private readonly IRepository<LeaveRequest, Guid> _leaveRequestRepository;

        public LeaveRequestsAppServiceTests()
        {
            _leaveRequestsAppService = GetRequiredService<ILeaveRequestsAppService>();
            _leaveRequestRepository = GetRequiredService<IRepository<LeaveRequest, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _leaveRequestsAppService.GetListAsync(new GetLeaveRequestsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.LeaveRequest.Id == Guid.Parse("bffcad48-3c25-419a-9b9b-ef7023dc4f66")).ShouldBe(true);
            result.Items.Any(x => x.LeaveRequest.Id == Guid.Parse("858c59ae-3c80-4f77-a163-276bf63d65a1")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _leaveRequestsAppService.GetAsync(Guid.Parse("bffcad48-3c25-419a-9b9b-ef7023dc4f66"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("bffcad48-3c25-419a-9b9b-ef7023dc4f66"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new LeaveRequestCreateDto
            {
                LeaveRequestType = default,
                StartDate = new DateTime(2007, 2, 6),
                EndDate = new DateTime(2006, 11, 2),
                LeaveRequestStatus = default,
                RequestedOn = new DateTime(2003, 4, 2),
                ReviewedOn = new DateTime(2005, 2, 2),
                WorkflowInstanceId = "3e3fae9be3134314be19daa0ddd5c6d78d916a5e755c45ad8466ec93e7440746ea47b7e5e7304758a25c6e0d029200faf681b102dc214d3a9b68fa22aaf621040bffb00c700f4d76bd1d704dd1fe10775ed5a4897a0d4727b42ff9ecba1d0e058e4a8843050d4e3e8490af61201b5b29cf5227aebd6b49a59fe83a5ca504a6c386bee114f21b49f2968315265d1ad7adf0b6f390a4654a83a9f044d2bc18f05e82eea986082a40df8a00d101b9492380f19ee909ac944f6d9a75cd351ee21e3f3df1a30f22264a8da574d5d8bc031b5493077f275ca0466e92a5b6f225c917372e084d314da54a49875b906794396d64daa524954b9346bda7c7856e5c135b876a0d1d40b4384fab9f58aed61717dc71ee529147abb04c04b907ec23f72c390b118083885c444f3ab51ce9495a3be4a6d64f727e021f491386c804d13ac56cc9a06689b70a8e4e549e17fda1c8e9ab5295db7a2f6bd74aa1ad4f2fdb2ab52983f818b23826d04c3d8ce4464d89a30fecdc60916167e145f2b5dd31bdd00e3b6ff4b4007f45a140318a0698a6dbf8fd6ef1b13c5808d641fca1ae1bdb0be81e85b849bb8acf194e37ae29622192aa2679c16dfd8fa0a3483eb6f408546bb607c270463b93a91545b48045f176563bc6e252b5fd096a37498bb418ef8bdfc76444b8c9379ea01a4823bacc8cfe31de5793437dc5b5",
                EmployeeId = Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc"),

            };

            // Act
            var serviceResult = await _leaveRequestsAppService.CreateAsync(input);

            // Assert
            var result = await _leaveRequestRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.LeaveRequestType.ShouldBe(default);
            result.StartDate.ShouldBe(new DateTime(2007, 2, 6));
            result.EndDate.ShouldBe(new DateTime(2006, 11, 2));
            result.LeaveRequestStatus.ShouldBe(default);
            result.RequestedOn.ShouldBe(new DateTime(2003, 4, 2));
            result.ReviewedOn.ShouldBe(new DateTime(2005, 2, 2));
            result.WorkflowInstanceId.ShouldBe("3e3fae9be3134314be19daa0ddd5c6d78d916a5e755c45ad8466ec93e7440746ea47b7e5e7304758a25c6e0d029200faf681b102dc214d3a9b68fa22aaf621040bffb00c700f4d76bd1d704dd1fe10775ed5a4897a0d4727b42ff9ecba1d0e058e4a8843050d4e3e8490af61201b5b29cf5227aebd6b49a59fe83a5ca504a6c386bee114f21b49f2968315265d1ad7adf0b6f390a4654a83a9f044d2bc18f05e82eea986082a40df8a00d101b9492380f19ee909ac944f6d9a75cd351ee21e3f3df1a30f22264a8da574d5d8bc031b5493077f275ca0466e92a5b6f225c917372e084d314da54a49875b906794396d64daa524954b9346bda7c7856e5c135b876a0d1d40b4384fab9f58aed61717dc71ee529147abb04c04b907ec23f72c390b118083885c444f3ab51ce9495a3be4a6d64f727e021f491386c804d13ac56cc9a06689b70a8e4e549e17fda1c8e9ab5295db7a2f6bd74aa1ad4f2fdb2ab52983f818b23826d04c3d8ce4464d89a30fecdc60916167e145f2b5dd31bdd00e3b6ff4b4007f45a140318a0698a6dbf8fd6ef1b13c5808d641fca1ae1bdb0be81e85b849bb8acf194e37ae29622192aa2679c16dfd8fa0a3483eb6f408546bb607c270463b93a91545b48045f176563bc6e252b5fd096a37498bb418ef8bdfc76444b8c9379ea01a4823bacc8cfe31de5793437dc5b5");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new LeaveRequestUpdateDto()
            {
                LeaveRequestType = default,
                StartDate = new DateTime(2000, 1, 3),
                EndDate = new DateTime(2005, 1, 13),
                LeaveRequestStatus = default,
                RequestedOn = new DateTime(2012, 8, 19),
                ReviewedOn = new DateTime(2023, 10, 5),
                WorkflowInstanceId = "3dafb5ea7d734e069e280ddd6676d6f910c5e31e0eec41ffa94b7070efdfa76cef47d481834e482994a1250338157cd917f3a8ab230645539052c6a6db64317b318646f3b8264c769a547cc4a74a27abc953bd489c21400db5acc0368b317622e5f242d6869e4e92903ae626507114cb5f0cf154297943e9ae606c6fdef04316b118883465114c86b50d4936c54cadf41ae7fc4551d843adbb5e57b5078f062731dee6e1fdcd41c38fed4c963d85d3a04745593adce44229914371e3dd9d6f25d8ed52e4508e45a1b07cb6ddd3755c282acedbdbd74c40a2b03aa009ca62a777d4cdbb372cb241e48403eda3e8460d8fff82a87bf99247ecbf515d1d1298c1e3d963da540dfc44a8a1ef7dfae2fb6841fdc511ca2ddb479eb572ec13cf3bad96e7c3c048dfff456eab4a57eb6d555d5cedd896be04c6439cac1c6c9a5babac7df100f1355c2344b3914d4441541797714b6fc022afbc46a086e939b7303b79fc43d7fd790a4f45c3a038655ed0abb124ef7d7437584b4528a2e31320b38af74c4d10c6416fc34c62bdf0ad4fb1b042e05029d1f9319d47ffa4aaccf84ffce366aa26c8cfd97a4ff3b4c6f4eaea95813aae9f58626c524082935e13e1933f1ad4c1964d6403d3429b869967b1785af1d274c7198ae9364ce69555cd3eef8f4538f1d955ae93e84cf781b7b44217d00cca99d0d796",
                EmployeeId = Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc"),

            };

            // Act
            var serviceResult = await _leaveRequestsAppService.UpdateAsync(Guid.Parse("bffcad48-3c25-419a-9b9b-ef7023dc4f66"), input);

            // Assert
            var result = await _leaveRequestRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.LeaveRequestType.ShouldBe(default);
            result.StartDate.ShouldBe(new DateTime(2000, 1, 3));
            result.EndDate.ShouldBe(new DateTime(2005, 1, 13));
            result.LeaveRequestStatus.ShouldBe(default);
            result.RequestedOn.ShouldBe(new DateTime(2012, 8, 19));
            result.ReviewedOn.ShouldBe(new DateTime(2023, 10, 5));
            result.WorkflowInstanceId.ShouldBe("3dafb5ea7d734e069e280ddd6676d6f910c5e31e0eec41ffa94b7070efdfa76cef47d481834e482994a1250338157cd917f3a8ab230645539052c6a6db64317b318646f3b8264c769a547cc4a74a27abc953bd489c21400db5acc0368b317622e5f242d6869e4e92903ae626507114cb5f0cf154297943e9ae606c6fdef04316b118883465114c86b50d4936c54cadf41ae7fc4551d843adbb5e57b5078f062731dee6e1fdcd41c38fed4c963d85d3a04745593adce44229914371e3dd9d6f25d8ed52e4508e45a1b07cb6ddd3755c282acedbdbd74c40a2b03aa009ca62a777d4cdbb372cb241e48403eda3e8460d8fff82a87bf99247ecbf515d1d1298c1e3d963da540dfc44a8a1ef7dfae2fb6841fdc511ca2ddb479eb572ec13cf3bad96e7c3c048dfff456eab4a57eb6d555d5cedd896be04c6439cac1c6c9a5babac7df100f1355c2344b3914d4441541797714b6fc022afbc46a086e939b7303b79fc43d7fd790a4f45c3a038655ed0abb124ef7d7437584b4528a2e31320b38af74c4d10c6416fc34c62bdf0ad4fb1b042e05029d1f9319d47ffa4aaccf84ffce366aa26c8cfd97a4ff3b4c6f4eaea95813aae9f58626c524082935e13e1933f1ad4c1964d6403d3429b869967b1785af1d274c7198ae9364ce69555cd3eef8f4538f1d955ae93e84cf781b7b44217d00cca99d0d796");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _leaveRequestsAppService.DeleteAsync(Guid.Parse("bffcad48-3c25-419a-9b9b-ef7023dc4f66"));

            // Assert
            var result = await _leaveRequestRepository.FindAsync(c => c.Id == Guid.Parse("bffcad48-3c25-419a-9b9b-ef7023dc4f66"));

            result.ShouldBeNull();
        }
    }
}