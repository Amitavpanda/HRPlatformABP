using HRManagement.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using HRManagement.Shared;

namespace HRManagement.AttendanceLogs
{
    public partial interface IAttendanceLogsAppService : IApplicationService
    {

        Task<PagedResultDto<AttendanceLogWithNavigationPropertiesDto>> GetListAsync(GetAttendanceLogsInput input);

        Task<AttendanceLogWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<AttendanceLogDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<AttendanceLogDto> CreateAsync(AttendanceLogCreateDto input);

        Task<AttendanceLogDto> UpdateAsync(Guid id, AttendanceLogUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(AttendanceLogExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> attendancelogIds);

        Task DeleteAllAsync(GetAttendanceLogsInput input);
        Task<HRManagement.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}