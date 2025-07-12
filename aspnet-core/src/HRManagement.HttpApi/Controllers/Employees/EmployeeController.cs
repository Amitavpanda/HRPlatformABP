using HRManagement.Shared;
using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HRManagement.Employees;
using Volo.Abp.Content;
using HRManagement.Shared;

namespace HRManagement.Controllers.Employees
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Employee")]
    [Route("api/app/employees")]

    public abstract class EmployeeControllerBase : AbpController
    {
        protected IEmployeesAppService _employeesAppService;

        public EmployeeControllerBase(IEmployeesAppService employeesAppService)
        {
            _employeesAppService = employeesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<EmployeeWithNavigationPropertiesDto>> GetListAsync(GetEmployeesInput input)
        {
            return _employeesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<EmployeeWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _employeesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<EmployeeDto> GetAsync(Guid id)
        {
            return _employeesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("identity-user-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input)
        {
            return _employeesAppService.GetIdentityUserLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<EmployeeDto> CreateAsync(EmployeeCreateDto input)
        {
            return _employeesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<EmployeeDto> UpdateAsync(Guid id, EmployeeUpdateDto input)
        {
            return _employeesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _employeesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeExcelDownloadDto input)
        {
            return _employeesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<HRManagement.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _employeesAppService.GetDownloadTokenAsync();
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> employeeIds)
        {
            return _employeesAppService.DeleteByIdsAsync(employeeIds);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetEmployeesInput input)
        {
            return _employeesAppService.DeleteAllAsync(input);
        }
    }
}