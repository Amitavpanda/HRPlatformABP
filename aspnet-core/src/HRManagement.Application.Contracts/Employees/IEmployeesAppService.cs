using HRManagement.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using HRManagement.Shared;

namespace HRManagement.Employees
{
    public partial interface IEmployeesAppService : IApplicationService
    {

        Task<PagedResultDto<EmployeeWithNavigationPropertiesDto>> GetListAsync(GetEmployeesInput input);

        Task<EmployeeWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<EmployeeDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<EmployeeDto> CreateAsync(EmployeeCreateDto input);

        Task<EmployeeDto> UpdateAsync(Guid id, EmployeeUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> employeeIds);

        Task DeleteAllAsync(GetEmployeesInput input);
        Task<HRManagement.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}