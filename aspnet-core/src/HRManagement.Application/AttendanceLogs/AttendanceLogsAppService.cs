using HRManagement.Shared;
using HRManagement.Employees;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using HRManagement.Permissions;
using HRManagement.AttendanceLogs;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using HRManagement.Shared;

namespace HRManagement.AttendanceLogs
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HRManagementPermissions.AttendanceLogs.Default)]
    public abstract class AttendanceLogsAppServiceBase : HRManagementAppService
    {
        protected IDistributedCache<AttendanceLogDownloadTokenCacheItem, string> _downloadTokenCache;
        protected IAttendanceLogRepository _attendanceLogRepository;
        protected AttendanceLogManager _attendanceLogManager;

        protected IRepository<HRManagement.Employees.Employee, Guid> _employeeRepository;

        public AttendanceLogsAppServiceBase(IAttendanceLogRepository attendanceLogRepository, AttendanceLogManager attendanceLogManager, IDistributedCache<AttendanceLogDownloadTokenCacheItem, string> downloadTokenCache, IRepository<HRManagement.Employees.Employee, Guid> employeeRepository)
        {
            _downloadTokenCache = downloadTokenCache;
            _attendanceLogRepository = attendanceLogRepository;
            _attendanceLogManager = attendanceLogManager; _employeeRepository = employeeRepository;

        }

        public virtual async Task<PagedResultDto<AttendanceLogWithNavigationPropertiesDto>> GetListAsync(GetAttendanceLogsInput input)
        {
            var totalCount = await _attendanceLogRepository.GetCountAsync(input.FilterText, input.DateMin, input.DateMax, input.CheckInTimeMin, input.CheckInTimeMax, input.CheckOutTimeMin, input.CheckOutTimeMax, input.Status, input.EmployeeId);
            var items = await _attendanceLogRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.DateMin, input.DateMax, input.CheckInTimeMin, input.CheckInTimeMax, input.CheckOutTimeMin, input.CheckOutTimeMax, input.Status, input.EmployeeId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AttendanceLogWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AttendanceLogWithNavigationProperties>, List<AttendanceLogWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<AttendanceLogWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<AttendanceLogWithNavigationProperties, AttendanceLogWithNavigationPropertiesDto>
                (await _attendanceLogRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<AttendanceLogDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<AttendanceLog, AttendanceLogDto>(await _attendanceLogRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeLookupAsync(LookupRequestDto input)
        {
            var query = (await _employeeRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.EmployeeNumber != null &&
                         x.EmployeeNumber.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<HRManagement.Employees.Employee>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<HRManagement.Employees.Employee>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(HRManagementPermissions.AttendanceLogs.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _attendanceLogRepository.DeleteAsync(id);
        }

        [Authorize(HRManagementPermissions.AttendanceLogs.Create)]
        public virtual async Task<AttendanceLogDto> CreateAsync(AttendanceLogCreateDto input)
        {
            if (input.EmployeeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Employee"]]);
            }

            var attendanceLog = await _attendanceLogManager.CreateAsync(
            input.EmployeeId, input.Date, input.CheckInTime, input.CheckOutTime, input.Status
            );

            return ObjectMapper.Map<AttendanceLog, AttendanceLogDto>(attendanceLog);
        }

        [Authorize(HRManagementPermissions.AttendanceLogs.Edit)]
        public virtual async Task<AttendanceLogDto> UpdateAsync(Guid id, AttendanceLogUpdateDto input)
        {
            if (input.EmployeeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Employee"]]);
            }

            var attendanceLog = await _attendanceLogManager.UpdateAsync(
            id,
            input.EmployeeId, input.Date, input.CheckInTime, input.CheckOutTime, input.Status, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<AttendanceLog, AttendanceLogDto>(attendanceLog);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(AttendanceLogExcelDownloadDto input)
        {
            var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var attendanceLogs = await _attendanceLogRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.DateMin, input.DateMax, input.CheckInTimeMin, input.CheckInTimeMax, input.CheckOutTimeMin, input.CheckOutTimeMax, input.Status, input.EmployeeId);
            var items = attendanceLogs.Select(item => new
            {
                Date = item.AttendanceLog.Date,
                CheckInTime = item.AttendanceLog.CheckInTime,
                CheckOutTime = item.AttendanceLog.CheckOutTime,
                Status = item.AttendanceLog.Status,

                Employee = item.Employee?.EmployeeNumber,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "AttendanceLogs.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(HRManagementPermissions.AttendanceLogs.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> attendancelogIds)
        {
            await _attendanceLogRepository.DeleteManyAsync(attendancelogIds);
        }

        [Authorize(HRManagementPermissions.AttendanceLogs.Delete)]
        public virtual async Task DeleteAllAsync(GetAttendanceLogsInput input)
        {
            await _attendanceLogRepository.DeleteAllAsync(input.FilterText, input.DateMin, input.DateMax, input.CheckInTimeMin, input.CheckInTimeMax, input.CheckOutTimeMin, input.CheckOutTimeMax, input.Status, input.EmployeeId);
        }
        public virtual async Task<HRManagement.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _downloadTokenCache.SetAsync(
                token,
                new AttendanceLogDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new HRManagement.Shared.DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}