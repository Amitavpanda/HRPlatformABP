using HRManagement.Shared;
using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HRManagement.LeaveRequests;
using Volo.Abp.Content;
using HRManagement.Shared;

namespace HRManagement.Controllers.LeaveRequests
{
    [RemoteService]
    [Area("app")]
    [ControllerName("LeaveRequest")]
    [Route("api/app/leave-requests")]

    public abstract class LeaveRequestControllerBase : AbpController
    {
        protected ILeaveRequestsAppService _leaveRequestsAppService;

        public LeaveRequestControllerBase(ILeaveRequestsAppService leaveRequestsAppService)
        {
            _leaveRequestsAppService = leaveRequestsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<LeaveRequestWithNavigationPropertiesDto>> GetListAsync(GetLeaveRequestsInput input)
        {
            return _leaveRequestsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<LeaveRequestWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _leaveRequestsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<LeaveRequestDto> GetAsync(Guid id)
        {
            return _leaveRequestsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("employee-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeLookupAsync(LookupRequestDto input)
        {
            return _leaveRequestsAppService.GetEmployeeLookupAsync(input);
        }

        [HttpGet]
        [Route("identity-user-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input)
        {
            return _leaveRequestsAppService.GetIdentityUserLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<LeaveRequestDto> CreateAsync(LeaveRequestCreateDto input)
        {
            return _leaveRequestsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<LeaveRequestDto> UpdateAsync(Guid id, LeaveRequestUpdateDto input)
        {
            return _leaveRequestsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _leaveRequestsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(LeaveRequestExcelDownloadDto input)
        {
            return _leaveRequestsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<HRManagement.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _leaveRequestsAppService.GetDownloadTokenAsync();
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> leaverequestIds)
        {
            return _leaveRequestsAppService.DeleteByIdsAsync(leaverequestIds);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetLeaveRequestsInput input)
        {
            return _leaveRequestsAppService.DeleteAllAsync(input);
        }
    }
}