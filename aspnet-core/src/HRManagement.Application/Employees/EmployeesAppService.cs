using HRManagement.Shared;
using Volo.Abp.Identity;
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
using HRManagement.Employees;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using HRManagement.Shared;

namespace HRManagement.Employees
{
    [RemoteService(IsEnabled = false)]
    
    public abstract class EmployeesAppServiceBase : HRManagementAppService
    {
        protected IDistributedCache<EmployeeDownloadTokenCacheItem, string> _downloadTokenCache;
        protected IEmployeeRepository _employeeRepository;
        protected EmployeeManager _employeeManager;

        protected IRepository<Volo.Abp.Identity.IdentityUser, Guid> _identityUserRepository;

        public EmployeesAppServiceBase(IEmployeeRepository employeeRepository, EmployeeManager employeeManager, IDistributedCache<EmployeeDownloadTokenCacheItem, string> downloadTokenCache, IRepository<Volo.Abp.Identity.IdentityUser, Guid> identityUserRepository)
        {
            _downloadTokenCache = downloadTokenCache;
            _employeeRepository = employeeRepository;
            _employeeManager = employeeManager; _identityUserRepository = identityUserRepository;

        }

        public virtual async Task<PagedResultDto<EmployeeWithNavigationPropertiesDto>> GetListAsync(GetEmployeesInput input)
        {
            var totalCount = await _employeeRepository.GetCountAsync(input.FilterText, input.EmployeeNumber, input.DateOfJoiningMin, input.DateOfJoiningMax, input.PaidLeaveBalanceMin, input.PaidLeaveBalanceMax, input.BaseSalaryMin, input.BaseSalaryMax, input.UnpaidLeaveBalanceMin, input.UnpaidLeaveBalanceMax, input.SickLeaveBalanceMin, input.SickLeaveBalanceMax, input.DeductionPerDayMin, input.DeductionPerDayMax, input.IdentityUserId);
            var items = await _employeeRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.EmployeeNumber, input.DateOfJoiningMin, input.DateOfJoiningMax, input.PaidLeaveBalanceMin, input.PaidLeaveBalanceMax, input.BaseSalaryMin, input.BaseSalaryMax, input.UnpaidLeaveBalanceMin, input.UnpaidLeaveBalanceMax, input.SickLeaveBalanceMin, input.SickLeaveBalanceMax, input.DeductionPerDayMin, input.DeductionPerDayMax, input.IdentityUserId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<EmployeeWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<EmployeeWithNavigationProperties>, List<EmployeeWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<EmployeeWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeWithNavigationProperties, EmployeeWithNavigationPropertiesDto>
                (await _employeeRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<EmployeeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Employee, EmployeeDto>(await _employeeRepository.GetAsync(id));
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

        [Authorize(HRManagementPermissions.Employees.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _employeeRepository.DeleteAsync(id);
        }

        [Authorize(HRManagementPermissions.Employees.Create)]
        public virtual async Task<EmployeeDto> CreateAsync(EmployeeCreateDto input)
        {

            var employee = await _employeeManager.CreateAsync(
            input.IdentityUserId, input.DateOfJoining, input.PaidLeaveBalance, input.BaseSalary, input.UnpaidLeaveBalance, input.SickLeaveBalance, input.DeductionPerDay, input.EmployeeNumber
            );

            return ObjectMapper.Map<Employee, EmployeeDto>(employee);
        }

        [Authorize(HRManagementPermissions.Employees.Edit)]
        public virtual async Task<EmployeeDto> UpdateAsync(Guid id, EmployeeUpdateDto input)
        {

            var employee = await _employeeManager.UpdateAsync(
            id,
            input.IdentityUserId, input.DateOfJoining, input.PaidLeaveBalance, input.BaseSalary, input.UnpaidLeaveBalance, input.SickLeaveBalance, input.DeductionPerDay, input.EmployeeNumber, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Employee, EmployeeDto>(employee);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeExcelDownloadDto input)
        {
            var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var employees = await _employeeRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.EmployeeNumber, input.DateOfJoiningMin, input.DateOfJoiningMax, input.PaidLeaveBalanceMin, input.PaidLeaveBalanceMax, input.BaseSalaryMin, input.BaseSalaryMax, input.UnpaidLeaveBalanceMin, input.UnpaidLeaveBalanceMax, input.SickLeaveBalanceMin, input.SickLeaveBalanceMax, input.DeductionPerDayMin, input.DeductionPerDayMax, input.IdentityUserId);
            var items = employees.Select(item => new
            {
                EmployeeNumber = item.Employee.EmployeeNumber,
                DateOfJoining = item.Employee.DateOfJoining,
                PaidLeaveBalance = item.Employee.PaidLeaveBalance,
                BaseSalary = item.Employee.BaseSalary,
                UnpaidLeaveBalance = item.Employee.UnpaidLeaveBalance,
                SickLeaveBalance = item.Employee.SickLeaveBalance,
                DeductionPerDay = item.Employee.DeductionPerDay,

                IdentityUser = item.IdentityUser?.Name,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Employees.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(HRManagementPermissions.Employees.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> employeeIds)
        {
            await _employeeRepository.DeleteManyAsync(employeeIds);
        }

        [Authorize(HRManagementPermissions.Employees.Delete)]
        public virtual async Task DeleteAllAsync(GetEmployeesInput input)
        {
            await _employeeRepository.DeleteAllAsync(input.FilterText, input.EmployeeNumber, input.DateOfJoiningMin, input.DateOfJoiningMax, input.PaidLeaveBalanceMin, input.PaidLeaveBalanceMax, input.BaseSalaryMin, input.BaseSalaryMax, input.UnpaidLeaveBalanceMin, input.UnpaidLeaveBalanceMax, input.SickLeaveBalanceMin, input.SickLeaveBalanceMax, input.DeductionPerDayMin, input.DeductionPerDayMax, input.IdentityUserId);
        }
        public virtual async Task<HRManagement.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _downloadTokenCache.SetAsync(
                token,
                new EmployeeDownloadTokenCacheItem { Token = token },
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