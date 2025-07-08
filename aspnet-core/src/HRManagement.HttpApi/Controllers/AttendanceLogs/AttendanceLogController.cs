using HRManagement.Shared;
using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HRManagement.AttendanceLogs;
using Volo.Abp.Content;
using HRManagement.Shared;

namespace HRManagement.Controllers.AttendanceLogs
{
    [RemoteService]
    [Area("app")]
    [ControllerName("AttendanceLog")]
    [Route("api/app/attendance-logs")]

    public abstract class AttendanceLogControllerBase : AbpController
    {
        protected IAttendanceLogsAppService _attendanceLogsAppService;

        public AttendanceLogControllerBase(IAttendanceLogsAppService attendanceLogsAppService)
        {
            _attendanceLogsAppService = attendanceLogsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<AttendanceLogWithNavigationPropertiesDto>> GetListAsync(GetAttendanceLogsInput input)
        {
            return _attendanceLogsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<AttendanceLogWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _attendanceLogsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<AttendanceLogDto> GetAsync(Guid id)
        {
            return _attendanceLogsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("employee-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeLookupAsync(LookupRequestDto input)
        {
            return _attendanceLogsAppService.GetEmployeeLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<AttendanceLogDto> CreateAsync(AttendanceLogCreateDto input)
        {
            return _attendanceLogsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<AttendanceLogDto> UpdateAsync(Guid id, AttendanceLogUpdateDto input)
        {
            return _attendanceLogsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _attendanceLogsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(AttendanceLogExcelDownloadDto input)
        {
            return _attendanceLogsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<HRManagement.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _attendanceLogsAppService.GetDownloadTokenAsync();
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> attendancelogIds)
        {
            return _attendanceLogsAppService.DeleteByIdsAsync(attendancelogIds);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetAttendanceLogsInput input)
        {
            return _attendanceLogsAppService.DeleteAllAsync(input);
        }
    }
}