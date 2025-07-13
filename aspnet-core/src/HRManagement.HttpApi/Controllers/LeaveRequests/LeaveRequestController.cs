using Asp.Versioning;
using HRManagement.Employees;
using HRManagement.LeaveRequests;
using HRManagement.Shared;
using HRManagement.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;
using Volo.Abp.ObjectMapping;
using static HRManagement.Permissions.HRManagementPermissions;
using Volo.Abp.Domain.Repositories;

namespace HRManagement.Controllers.LeaveRequests
{
    [RemoteService]
    [Area("app")]
    [ControllerName("LeaveRequest")]
    [Route("api/app/leave-requests")]
    [AllowAnonymous]

    public abstract class LeaveRequestControllerBase : AbpController
    {
        protected ILeaveRequestsAppService _leaveRequestsAppService;
        protected IEmployeesAppService _employeesAppService;

        public LeaveRequestControllerBase(ILeaveRequestsAppService leaveRequestsAppService, IEmployeesAppService employeesAppService)
        {
            _leaveRequestsAppService = leaveRequestsAppService;
            _employeesAppService = employeesAppService;
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


        [HttpGet("pending")]
        public virtual Task<PagedResultDto<LeaveRequestWithNavigationPropertiesDto>> GetPendingAsync(GetLeaveRequestsInput input)
        {
            return _leaveRequestsAppService.GetPendingAsync(input);
        }

        //Approve a leave request
        [HttpPost("{id}/approve")]
        public virtual Task<LeaveRequestWithNavigationPropertiesDto> ApproveAsync(Guid id, [FromQuery] Guid hrManagerId)
        {
            return _leaveRequestsAppService.ApproveAsync(id, hrManagerId);
        }

        // Reject a leave request
        [HttpPost("{id}/reject")]
        public virtual Task<LeaveRequestWithNavigationPropertiesDto> RejectAsync(Guid id, [FromQuery] Guid hrManagerId)
        {
            return _leaveRequestsAppService.RejectAsync(id, hrManagerId);
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

        [AllowAnonymous]
        [HttpGet]
        [Route("employee-leave-balance/{employeeId}")]
        public virtual async Task<IActionResult> GetLeaveBalance(Guid employeeId, [FromQuery] int leaveRequestType)
        {
            // You may have a method like GetAsync on EmployeeAppService
            var employee = await _employeesAppService.GetAsync(employeeId);
            if (employee == null)
                return NotFound();

            // Make sure your EmployeeDto (or entity) has a LeaveBalance property
            decimal balance = 0;
            switch (leaveRequestType)
            {
                case 1:
                    balance = employee.PaidLeaveBalance;
                    break;
                case 0:
                    balance = employee.SickLeaveBalance;
                    break;
                case 2:
                    balance = employee.UnpaidLeaveBalance; // Unpaid leave doesn't require balance, but you may return 0 or a special value.
                    break;
                default:
                    return BadRequest("Invalid leave type.");
            }
            return Ok(new { balance });
        }
        


        [HttpPost]
        public virtual Task<CreateLeaveRequestResultDto> CreateAsync(LeaveRequestCreateDto input)
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