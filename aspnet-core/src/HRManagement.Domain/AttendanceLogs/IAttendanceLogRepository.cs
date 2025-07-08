using HRManagement;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HRManagement.AttendanceLogs
{
    public partial interface IAttendanceLogRepository : IRepository<AttendanceLog, Guid>
    {

        Task DeleteAllAsync(
            string? filterText = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            TimeOnly? checkInTimeMin = null,
            TimeOnly? checkInTimeMax = null,
            TimeOnly? checkOutTimeMin = null,
            TimeOnly? checkOutTimeMax = null,
            AttendanceStatus? status = null,
            Guid? employeeId = null,
            CancellationToken cancellationToken = default);
        Task<AttendanceLogWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );

        Task<List<AttendanceLogWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            TimeOnly? checkInTimeMin = null,
            TimeOnly? checkInTimeMax = null,
            TimeOnly? checkOutTimeMin = null,
            TimeOnly? checkOutTimeMax = null,
            AttendanceStatus? status = null,
            Guid? employeeId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<AttendanceLog>> GetListAsync(
                    string? filterText = null,
                    DateTime? dateMin = null,
                    DateTime? dateMax = null,
                    TimeOnly? checkInTimeMin = null,
                    TimeOnly? checkInTimeMax = null,
                    TimeOnly? checkOutTimeMin = null,
                    TimeOnly? checkOutTimeMax = null,
                    AttendanceStatus? status = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            TimeOnly? checkInTimeMin = null,
            TimeOnly? checkInTimeMax = null,
            TimeOnly? checkOutTimeMin = null,
            TimeOnly? checkOutTimeMax = null,
            AttendanceStatus? status = null,
            Guid? employeeId = null,
            CancellationToken cancellationToken = default);
    }
}