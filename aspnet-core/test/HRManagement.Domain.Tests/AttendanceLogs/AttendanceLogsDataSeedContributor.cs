using HRManagement.Employees;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using HRManagement.AttendanceLogs;

namespace HRManagement.AttendanceLogs
{
    public class AttendanceLogsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IAttendanceLogRepository _attendanceLogRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly EmployeesDataSeedContributor _employeesDataSeedContributor;

        public AttendanceLogsDataSeedContributor(IAttendanceLogRepository attendanceLogRepository, IUnitOfWorkManager unitOfWorkManager, EmployeesDataSeedContributor employeesDataSeedContributor)
        {
            _attendanceLogRepository = attendanceLogRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _employeesDataSeedContributor = employeesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _employeesDataSeedContributor.SeedAsync(context);

            await _attendanceLogRepository.InsertAsync(new AttendanceLog
            (
                id: Guid.Parse("28e3d7d5-b3d4-47ce-b459-0545bb4b2178"),
                date: new DateTime(2016, 11, 27),
                checkInTime: TimeOnly.MinValue,
                checkOutTime: TimeOnly.MinValue,
                status: default,
                employeeId: Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc")
            ));

            await _attendanceLogRepository.InsertAsync(new AttendanceLog
            (
                id: Guid.Parse("f822fd2a-7adb-485d-aaa2-1453294e0ab7"),
                date: new DateTime(2015, 10, 22),
                checkInTime: TimeOnly.MinValue,
                checkOutTime: TimeOnly.MinValue,
                status: default,
                employeeId: Guid.Parse("c5f24531-de6f-4c97-a52a-57049cec3ecc")
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}