using HRManagement.Shared;
using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HRManagement.HRManagers;
using Volo.Abp.Content;
using HRManagement.Shared;

namespace HRManagement.Controllers.HRManagers
{
    [RemoteService]
    [Area("app")]
    [ControllerName("HRManager")]
    [Route("api/app/hrmanagers")]

    public abstract class HRManagerControllerBase : AbpController
    {
        protected IHRManagersAppService _hRManagersAppService;

        public HRManagerControllerBase(IHRManagersAppService hRManagersAppService)
        {
            _hRManagersAppService = hRManagersAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<HRManagerWithNavigationPropertiesDto>> GetListAsync(GetHRManagersInput input)
        {
            return _hRManagersAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<HRManagerWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _hRManagersAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<HRManagerDto> GetAsync(Guid id)
        {
            return _hRManagersAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("identity-user-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input)
        {
            return _hRManagersAppService.GetIdentityUserLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<HRManagerDto> CreateAsync(HRManagerCreateDto input)
        {
            return _hRManagersAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<HRManagerDto> UpdateAsync(Guid id, HRManagerUpdateDto input)
        {
            return _hRManagersAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _hRManagersAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(HRManagerExcelDownloadDto input)
        {
            return _hRManagersAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<HRManagement.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _hRManagersAppService.GetDownloadTokenAsync();
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> hrmanagerIds)
        {
            return _hRManagersAppService.DeleteByIdsAsync(hrmanagerIds);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetHRManagersInput input)
        {
            return _hRManagersAppService.DeleteAllAsync(input);
        }
    }
}