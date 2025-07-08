using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace HRManagement.AttendanceLogs
{
    public abstract class AttendanceLogsAppServiceTests<TStartupModule> : HRManagementApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IAttendanceLogsAppService _attendanceLogsAppService;
        private readonly IRepository<AttendanceLog, Guid> _attendanceLogRepository;

        public AttendanceLogsAppServiceTests()
        {
            _attendanceLogsAppService = GetRequiredService<IAttendanceLogsAppService>();
            _attendanceLogRepository = GetRequiredService<IRepository<AttendanceLog, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _attendanceLogsAppService.GetListAsync(new GetAttendanceLogsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.AttendanceLog.Id == Guid.Parse("28e3d7d5-b3d4-47ce-b459-0545bb4b2178")).ShouldBe(true);
            result.Items.Any(x => x.AttendanceLog.Id == Guid.Parse("f822fd2a-7adb-485d-aaa2-1453294e0ab7")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _attendanceLogsAppService.GetAsync(Guid.Parse("28e3d7d5-b3d4-47ce-b459-0545bb4b2178"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("28e3d7d5-b3d4-47ce-b459-0545bb4b2178"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new AttendanceLogCreateDto
            {
                Date = new DateTime(2006, 3, 21),
                CheckInTime = TimeOnly.MinValue,
                CheckOutTime = TimeOnly.MinValue,
                Status = default,
                EmployeeId = Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc")
            };

            // Act
            var serviceResult = await _attendanceLogsAppService.CreateAsync(input);

            // Assert
            var result = await _attendanceLogRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Date.ShouldBe(new DateTime(2006, 3, 21));
            result.CheckInTime.ShouldBe(TimeOnly.MinValue);
            result.CheckOutTime.ShouldBe(TimeOnly.MinValue);
            result.Status.ShouldBe(default);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new AttendanceLogUpdateDto()
            {
                Date = new DateTime(2021, 9, 5),
                CheckInTime = TimeOnly.MinValue,
                CheckOutTime = TimeOnly.MinValue,
                Status = default,
                EmployeeId = Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc")
            };

            // Act
            var serviceResult = await _attendanceLogsAppService.UpdateAsync(Guid.Parse("28e3d7d5-b3d4-47ce-b459-0545bb4b2178"), input);

            // Assert
            var result = await _attendanceLogRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Date.ShouldBe(new DateTime(2021, 9, 5));
            result.CheckInTime.ShouldBe(TimeOnly.MinValue);
            result.CheckOutTime.ShouldBe(TimeOnly.MinValue);
            result.Status.ShouldBe(default);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _attendanceLogsAppService.DeleteAsync(Guid.Parse("28e3d7d5-b3d4-47ce-b459-0545bb4b2178"));

            // Assert
            var result = await _attendanceLogRepository.FindAsync(c => c.Id == Guid.Parse("28e3d7d5-b3d4-47ce-b459-0545bb4b2178"));

            result.ShouldBeNull();
        }
    }
}