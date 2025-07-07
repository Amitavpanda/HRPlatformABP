using HRManagement.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using HRManagement.Shared;

namespace HRManagement.HRManagers
{
    public partial interface IHRManagersAppService : IApplicationService
    {

        Task<PagedResultDto<HRManagerWithNavigationPropertiesDto>> GetListAsync(GetHRManagersInput input);

        Task<HRManagerWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<HRManagerDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<HRManagerDto> CreateAsync(HRManagerCreateDto input);

        Task<HRManagerDto> UpdateAsync(Guid id, HRManagerUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(HRManagerExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> hrmanagerIds);

        Task DeleteAllAsync(GetHRManagersInput input);
        Task<HRManagement.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}