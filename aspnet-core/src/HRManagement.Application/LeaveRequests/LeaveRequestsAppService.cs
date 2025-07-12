using HRManagement.Employees;
using HRManagement.LeaveRequests;
using HRManagement.Permissions;
using HRManagement.Shared;
using HRManagement.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace HRManagement.LeaveRequests
{
    [RemoteService(IsEnabled = false)]

    public abstract class LeaveRequestsAppServiceBase : HRManagementAppService
    {
        protected IDistributedCache<LeaveRequestDownloadTokenCacheItem, string> _downloadTokenCache;
        protected ILeaveRequestRepository _leaveRequestRepository;
        protected LeaveRequestManager _leaveRequestManager;

        protected IRepository<HRManagement.Employees.Employee, Guid> _employeeRepository;
        protected IRepository<Volo.Abp.Identity.IdentityUser, Guid> _identityUserRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public LeaveRequestsAppServiceBase(IHttpClientFactory httpClientFactory, ILeaveRequestRepository leaveRequestRepository, LeaveRequestManager leaveRequestManager, IDistributedCache<LeaveRequestDownloadTokenCacheItem, string> downloadTokenCache, IRepository<HRManagement.Employees.Employee, Guid> employeeRepository, IRepository<Volo.Abp.Identity.IdentityUser, Guid> identityUserRepository)
        {
            _downloadTokenCache = downloadTokenCache;
            _leaveRequestRepository = leaveRequestRepository;
            _leaveRequestManager = leaveRequestManager; _employeeRepository = employeeRepository;
            _identityUserRepository = identityUserRepository;
            _httpClientFactory = httpClientFactory;

        }

        public virtual async Task<PagedResultDto<LeaveRequestWithNavigationPropertiesDto>> GetListAsync(GetLeaveRequestsInput input)
        {
            var totalCount = await _leaveRequestRepository.GetCountAsync(input.FilterText, input.LeaveRequestType, input.StartDateMin, input.StartDateMax, input.EndDateMin, input.EndDateMax, input.LeaveRequestStatus, input.RequestedOnMin, input.RequestedOnMax, input.ReviewedOnMin, input.ReviewedOnMax, input.WorkflowInstanceId, input.EmployeeId, input.ReviewedBy);
            var items = await _leaveRequestRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.LeaveRequestType, input.StartDateMin, input.StartDateMax, input.EndDateMin, input.EndDateMax, input.LeaveRequestStatus, input.RequestedOnMin, input.RequestedOnMax, input.ReviewedOnMin, input.ReviewedOnMax, input.WorkflowInstanceId, input.EmployeeId, input.ReviewedBy, input.Sorting, input.MaxResultCount, input.SkipCount);

            // Call Elsa workflow endpoint
            var httpClient = _httpClientFactory.CreateClient();
            var elsaResponse = await httpClient.GetAsync("https://localhost:44325/workflows/leave-requests/hello"); // Or your actual host/port
            var message = await elsaResponse.Content.ReadAsStringAsync();



            return new PagedResultWithMessageDto<LeaveRequestWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<LeaveRequestWithNavigationProperties>, List<LeaveRequestWithNavigationPropertiesDto>>(items),
                Message = message

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


        public virtual async Task<CreateLeaveRequestResultDto> CreateAsync(LeaveRequestCreateDto input)
        {
            try
            {
                if (input.EmployeeId == default)
                {
                    throw new UserFriendlyException(L["The {0} field is required.", L["Employee"]]);
                }
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var workflowEndpoint = "https://localhost:44325/workflows/leave-requests/validate";

                var payload = new
                {
                    EmployeeId = input.EmployeeId,
                    StartDate = input.StartDate,
                    EndDate = input.EndDate
                };
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Log the payload
                Logger.LogInformation("Sending payload to Elsa workflows: {@Payload}", payload);
                var response = await httpClient.PostAsync(workflowEndpoint, content);

                Logger.LogInformation("Received response from workflow: {StatusCode}", response.StatusCode);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Logger.LogError("Elsa workflow returned error status: {StatusCode} with content: {ErrorContent}", response.StatusCode, errorContent);

                    return new CreateLeaveRequestResultDto
                    {
                        WorkflowStatus = "Error",
                        LeaveRequest = null,
                        Message = $"Workflow error: {response.StatusCode}. Details: {errorContent}"
                    };
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                Logger.LogInformation("Workflow response content: {ResponseContent}", responseContent);

                Dictionary<string, string>? workflowResult = null;
                string status = "Rejected";

                try
                {
                    workflowResult = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);
                    status = workflowResult?["status"] ?? "Rejected";
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Failed to parse workflow response JSON. Raw response: {ResponseContent}", responseContent);
                }

                if (status == "Rejected")
                {
                    Logger.LogInformation("Leave request rejected due to workflow status: {Status}", status);
                    return new CreateLeaveRequestResultDto
                    {
                        WorkflowStatus = status,
                        LeaveRequest = null,
                        Message = "Leave request rejected due to insufficient leave balance."
                    };
                }

                var leaveRequest = await _leaveRequestManager.CreateAsync(
                    input.EmployeeId,
                    input.ReviewedBy,
                    input.LeaveRequestType,
                    input.StartDate,
                    input.EndDate,
                    input.LeaveRequestStatus,
                    input.RequestedOn,
                    input.ReviewedOn,
                    input.WorkflowInstanceId
                );

                var dto = ObjectMapper.Map<LeaveRequest, LeaveRequestDto>(leaveRequest);

                Logger.LogInformation("Leave request created successfully for EmployeeId: {EmployeeId}", input.EmployeeId);

                return new CreateLeaveRequestResultDto
                {
                    WorkflowStatus = status,
                    LeaveRequest = dto,
                    Message = "Leave request created successfully."
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to create leave request.");
                throw; // Or wrap in a UserFriendlyException if you want
            }
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