using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.AttendanceLogs;
using HRManagement.EntityFrameworkCore;
using Xunit;

namespace HRManagement.EntityFrameworkCore.Domains.AttendanceLogs
{
    public class AttendanceLogRepositoryTests : HRManagementEntityFrameworkCoreTestBase
    {
        private readonly IAttendanceLogRepository _attendanceLogRepository;

        public AttendanceLogRepositoryTests()
        {
            _attendanceLogRepository = GetRequiredService<IAttendanceLogRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _attendanceLogRepository.GetListAsync(
                    status: default
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("28e3d7d5-b3d4-47ce-b459-0545bb4b2178"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _attendanceLogRepository.GetCountAsync(
                    status: default
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}