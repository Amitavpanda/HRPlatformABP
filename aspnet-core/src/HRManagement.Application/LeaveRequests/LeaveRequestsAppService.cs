using HRManagement.Shared;
using Volo.Abp.Identity;
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
using HRManagement.LeaveRequests;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using HRManagement.Shared;

namespace HRManagement.LeaveRequests
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HRManagementPermissions.LeaveRequests.Default)]
    public abstract class LeaveRequestsAppServiceBase : HRManagementAppService
    {
        protected IDistributedCache<LeaveRequestDownloadTokenCacheItem, string> _downloadTokenCache;
        protected ILeaveRequestRepository _leaveRequestRepository;
        protected LeaveRequestManager _leaveRequestManager;

        protected IRepository<HRManagement.Employees.Employee, Guid> _employeeRepository;
        protected IRepository<Volo.Abp.Identity.IdentityUser, Guid> _identityUserRepository;

        public LeaveRequestsAppServiceBase(ILeaveRequestRepository leaveRequestRepository, LeaveRequestManager leaveRequestManager, IDistributedCache<LeaveRequestDownloadTokenCacheItem, string> downloadTokenCache, IRepository<HRManagement.Employees.Employee, Guid> employeeRepository, IRepository<Volo.Abp.Identity.IdentityUser, Guid> identityUserRepository)
        {
            _downloadTokenCache = downloadTokenCache;
            _leaveRequestRepository = leaveRequestRepository;
            _leaveRequestManager = leaveRequestManager; _employeeRepository = employeeRepository;
            _identityUserRepository = identityUserRepository;

        }

        public virtual async Task<PagedResultDto<LeaveRequestWithNavigationPropertiesDto>> GetListAsync(GetLeaveRequestsInput input)
        {
            var totalCount = await _leaveRequestRepository.GetCountAsync(input.FilterText, input.LeaveRequestType, input.StartDateMin, input.StartDateMax, input.EndDateMin, input.EndDateMax, input.LeaveRequestStatus, input.RequestedOnMin, input.RequestedOnMax, input.ReviewedOnMin, input.ReviewedOnMax, input.WorkflowInstanceId, input.EmployeeId, input.ReviewedBy);
            var items = await _leaveRequestRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.LeaveRequestType, input.StartDateMin, input.StartDateMax, input.EndDateMin, input.EndDateMax, input.LeaveRequestStatus, input.RequestedOnMin, input.RequestedOnMax, input.ReviewedOnMin, input.ReviewedOnMax, input.WorkflowInstanceId, input.EmployeeId, input.ReviewedBy, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<LeaveRequestWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<LeaveRequestWithNavigationProperties>, List<LeaveRequestWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<LeaveRequestWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<LeaveRequestWithNavigationProperties, LeaveRequestWithNavigationPropertiesDto>
                (await _leaveRequestRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LeaveRequestDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<LeaveRequest, LeaveRequestDto>(await _leaveRequestRepository.GetAsync(id));
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input)
        {
            var query = (await _identityUserRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Volo.Abp.Identity.IdentityUser>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Volo.Abp.Identity.IdentityUser>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(HRManagementPermissions.LeaveRequests.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _leaveRequestRepository.DeleteAsync(id);
        }

        [Authorize(HRManagementPermissions.LeaveRequests.Create)]
        public virtual async Task<LeaveRequestDto> CreateAsync(LeaveRequestCreateDto input)
        {
            if (input.EmployeeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Employee"]]);
            }

            var leaveRequest = await _leaveRequestManager.CreateAsync(
            input.EmployeeId, input.ReviewedBy, input.LeaveRequestType, input.StartDate, input.EndDate, input.LeaveRequestStatus, input.RequestedOn, input.ReviewedOn, input.WorkflowInstanceId
            );

            return ObjectMapper.Map<LeaveRequest, LeaveRequestDto>(leaveRequest);
        }

        [Authorize(HRManagementPermissions.LeaveRequests.Edit)]
        public virtual async Task<LeaveRequestDto> UpdateAsync(Guid id, LeaveRequestUpdateDto input)
        {
            if (input.EmployeeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Employee"]]);
            }

            var leaveRequest = await _leaveRequestManager.UpdateAsync(
            id,
            input.EmployeeId, input.ReviewedBy, input.LeaveRequestType, input.StartDate, input.EndDate, input.LeaveRequestStatus, input.RequestedOn, input.ReviewedOn, input.WorkflowInstanceId, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<LeaveRequest, LeaveRequestDto>(leaveRequest);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(LeaveRequestExcelDownloadDto input)
        {
            var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var leaveRequests = await _leaveRequestRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.LeaveRequestType, input.StartDateMin, input.StartDateMax, input.EndDateMin, input.EndDateMax, input.LeaveRequestStatus, input.RequestedOnMin, input.RequestedOnMax, input.ReviewedOnMin, input.ReviewedOnMax, input.WorkflowInstanceId, input.EmployeeId, input.ReviewedBy);
            var items = leaveRequests.Select(item => new
            {
                LeaveRequestType = item.LeaveRequest.LeaveRequestType,
                StartDate = item.LeaveRequest.StartDate,
                EndDate = item.LeaveRequest.EndDate,
                LeaveRequestStatus = item.LeaveRequest.LeaveRequestStatus,
                RequestedOn = item.LeaveRequest.RequestedOn,
                ReviewedOn = item.LeaveRequest.ReviewedOn,
                WorkflowInstanceId = item.LeaveRequest.WorkflowInstanceId,

                Employee = item.Employee?.EmployeeNumber,
                ReviewedBy = item.ReviewedBy?.Name,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "LeaveRequests.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(HRManagementPermissions.LeaveRequests.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> leaverequestIds)
        {
            await _leaveRequestRepository.DeleteManyAsync(leaverequestIds);
        }

        [Authorize(HRManagementPermissions.LeaveRequests.Delete)]
        public virtual async Task DeleteAllAsync(GetLeaveRequestsInput input)
        {
            await _leaveRequestRepository.DeleteAllAsync(input.FilterText, input.LeaveRequestType, input.StartDateMin, input.StartDateMax, input.EndDateMin, input.EndDateMax, input.LeaveRequestStatus, input.RequestedOnMin, input.RequestedOnMax, input.ReviewedOnMin, input.ReviewedOnMax, input.WorkflowInstanceId, input.EmployeeId, input.ReviewedBy);
        }
        public virtual async Task<HRManagement.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _downloadTokenCache.SetAsync(
                token,
                new LeaveRequestDownloadTokenCacheItem { Token = token },
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