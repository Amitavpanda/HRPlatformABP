using HRManagement.Shared;
using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HRManagement.PayrollRecords;
using Volo.Abp.Content;
using HRManagement.Shared;

namespace HRManagement.Controllers.PayrollRecords
{
    [RemoteService]
    [Area("app")]
    [ControllerName("PayrollRecord")]
    [Route("api/app/payroll-records")]

    public abstract class PayrollRecordControllerBase : AbpController
    {
        protected IPayrollRecordsAppService _payrollRecordsAppService;

        public PayrollRecordControllerBase(IPayrollRecordsAppService payrollRecordsAppService)
        {
            _payrollRecordsAppService = payrollRecordsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<PayrollRecordWithNavigationPropertiesDto>> GetListAsync(GetPayrollRecordsInput input)
        {
            return _payrollRecordsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<PayrollRecordWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _payrollRecordsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<PayrollRecordDto> GetAsync(Guid id)
        {
            return _payrollRecordsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("employee-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeLookupAsync(LookupRequestDto input)
        {
            return _payrollRecordsAppService.GetEmployeeLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<PayrollRecordDto> CreateAsync(PayrollRecordCreateDto input)
        {
            return _payrollRecordsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<PayrollRecordDto> UpdateAsync(Guid id, PayrollRecordUpdateDto input)
        {
            return _payrollRecordsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _payrollRecordsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(PayrollRecordExcelDownloadDto input)
        {
            return _payrollRecordsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<HRManagement.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _payrollRecordsAppService.GetDownloadTokenAsync();
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> payrollrecordIds)
        {
            return _payrollRecordsAppService.DeleteByIdsAsync(payrollrecordIds);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetPayrollRecordsInput input)
        {
            return _payrollRecordsAppService.DeleteAllAsync(input);
        }
    }
}