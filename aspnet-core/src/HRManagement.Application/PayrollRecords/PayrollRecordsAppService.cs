using HRManagement.Shared;
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
using HRManagement.PayrollRecords;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using HRManagement.Shared;

namespace HRManagement.PayrollRecords
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HRManagementPermissions.PayrollRecords.Default)]
    public abstract class PayrollRecordsAppServiceBase : HRManagementAppService
    {
        protected IDistributedCache<PayrollRecordDownloadTokenCacheItem, string> _downloadTokenCache;
        protected IPayrollRecordRepository _payrollRecordRepository;
        protected PayrollRecordManager _payrollRecordManager;

        protected IRepository<HRManagement.Employees.Employee, Guid> _employeeRepository;

        public PayrollRecordsAppServiceBase(IPayrollRecordRepository payrollRecordRepository, PayrollRecordManager payrollRecordManager, IDistributedCache<PayrollRecordDownloadTokenCacheItem, string> downloadTokenCache, IRepository<HRManagement.Employees.Employee, Guid> employeeRepository)
        {
            _downloadTokenCache = downloadTokenCache;
            _payrollRecordRepository = payrollRecordRepository;
            _payrollRecordManager = payrollRecordManager; _employeeRepository = employeeRepository;

        }

        public virtual async Task<PagedResultDto<PayrollRecordWithNavigationPropertiesDto>> GetListAsync(GetPayrollRecordsInput input)
        {
            var totalCount = await _payrollRecordRepository.GetCountAsync(input.FilterText, input.MonthMin, input.MonthMax, input.YearMin, input.YearMax, input.BaseSalaryMin, input.BaseSalaryMax, input.LeaveDeductionsMin, input.LeaveDeductionsMax, input.NetPayMin, input.NetPayMax, input.Status, input.PayslipUrl, input.EmployeeId);
            var items = await _payrollRecordRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.MonthMin, input.MonthMax, input.YearMin, input.YearMax, input.BaseSalaryMin, input.BaseSalaryMax, input.LeaveDeductionsMin, input.LeaveDeductionsMax, input.NetPayMin, input.NetPayMax, input.Status, input.PayslipUrl, input.EmployeeId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PayrollRecordWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PayrollRecordWithNavigationProperties>, List<PayrollRecordWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<PayrollRecordWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<PayrollRecordWithNavigationProperties, PayrollRecordWithNavigationPropertiesDto>
                (await _payrollRecordRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<PayrollRecordDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PayrollRecord, PayrollRecordDto>(await _payrollRecordRepository.GetAsync(id));
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

        [Authorize(HRManagementPermissions.PayrollRecords.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _payrollRecordRepository.DeleteAsync(id);
        }

        [Authorize(HRManagementPermissions.PayrollRecords.Create)]
        public virtual async Task<PayrollRecordDto> CreateAsync(PayrollRecordCreateDto input)
        {
            if (input.EmployeeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Employee"]]);
            }

            var payrollRecord = await _payrollRecordManager.CreateAsync(
            input.EmployeeId, input.Month, input.Year, input.BaseSalary, input.LeaveDeductions, input.NetPay, input.Status, input.PayslipUrl
            );

            return ObjectMapper.Map<PayrollRecord, PayrollRecordDto>(payrollRecord);
        }

        [Authorize(HRManagementPermissions.PayrollRecords.Edit)]
        public virtual async Task<PayrollRecordDto> UpdateAsync(Guid id, PayrollRecordUpdateDto input)
        {
            if (input.EmployeeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Employee"]]);
            }

            var payrollRecord = await _payrollRecordManager.UpdateAsync(
            id,
            input.EmployeeId, input.Month, input.Year, input.BaseSalary, input.LeaveDeductions, input.NetPay, input.Status, input.PayslipUrl, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<PayrollRecord, PayrollRecordDto>(payrollRecord);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PayrollRecordExcelDownloadDto input)
        {
            var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var payrollRecords = await _payrollRecordRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.MonthMin, input.MonthMax, input.YearMin, input.YearMax, input.BaseSalaryMin, input.BaseSalaryMax, input.LeaveDeductionsMin, input.LeaveDeductionsMax, input.NetPayMin, input.NetPayMax, input.Status, input.PayslipUrl, input.EmployeeId);
            var items = payrollRecords.Select(item => new
            {
                Month = item.PayrollRecord.Month,
                Year = item.PayrollRecord.Year,
                BaseSalary = item.PayrollRecord.BaseSalary,
                LeaveDeductions = item.PayrollRecord.LeaveDeductions,
                NetPay = item.PayrollRecord.NetPay,
                Status = item.PayrollRecord.Status,
                PayslipUrl = item.PayrollRecord.PayslipUrl,

                Employee = item.Employee?.EmployeeNumber,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "PayrollRecords.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(HRManagementPermissions.PayrollRecords.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> payrollrecordIds)
        {
            await _payrollRecordRepository.DeleteManyAsync(payrollrecordIds);
        }

        [Authorize(HRManagementPermissions.PayrollRecords.Delete)]
        public virtual async Task DeleteAllAsync(GetPayrollRecordsInput input)
        {
            await _payrollRecordRepository.DeleteAllAsync(input.FilterText, input.MonthMin, input.MonthMax, input.YearMin, input.YearMax, input.BaseSalaryMin, input.BaseSalaryMax, input.LeaveDeductionsMin, input.LeaveDeductionsMax, input.NetPayMin, input.NetPayMax, input.Status, input.PayslipUrl, input.EmployeeId);
        }
        public virtual async Task<HRManagement.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _downloadTokenCache.SetAsync(
                token,
                new PayrollRecordDownloadTokenCacheItem { Token = token },
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