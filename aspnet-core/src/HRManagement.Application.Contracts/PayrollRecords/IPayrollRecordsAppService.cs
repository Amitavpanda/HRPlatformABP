using HRManagement.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using HRManagement.Shared;

namespace HRManagement.PayrollRecords
{
    public partial interface IPayrollRecordsAppService : IApplicationService
    {

        Task<PagedResultDto<PayrollRecordWithNavigationPropertiesDto>> GetListAsync(GetPayrollRecordsInput input);

        Task<PayrollRecordWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<PayrollRecordDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<PayrollRecordDto> CreateAsync(PayrollRecordCreateDto input);

        Task<PayrollRecordDto> UpdateAsync(Guid id, PayrollRecordUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(PayrollRecordExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> payrollrecordIds);

        Task DeleteAllAsync(GetPayrollRecordsInput input);
        Task<HRManagement.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}