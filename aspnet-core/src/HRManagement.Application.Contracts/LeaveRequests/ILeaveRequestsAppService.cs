using HRManagement.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using HRManagement.Shared;

namespace HRManagement.LeaveRequests
{
    public partial interface ILeaveRequestsAppService : IApplicationService
    {

        Task<PagedResultDto<LeaveRequestWithNavigationPropertiesDto>> GetListAsync(GetLeaveRequestsInput input);

        Task<LeaveRequestWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LeaveRequestDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LeaveRequestWithNavigationPropertiesDto>> GetPendingAsync(GetLeaveRequestsInput input);
        Task<LeaveRequestWithNavigationPropertiesDto> ApproveAsync(Guid id, Guid hrManagerId);
        Task<LeaveRequestWithNavigationPropertiesDto> RejectAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CreateLeaveRequestResultDto> CreateAsync(LeaveRequestCreateDto input);

        Task<LeaveRequestDto> UpdateAsync(Guid id, LeaveRequestUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(LeaveRequestExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> leaverequestIds);

        Task DeleteAllAsync(GetLeaveRequestsInput input);
        Task<HRManagement.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}