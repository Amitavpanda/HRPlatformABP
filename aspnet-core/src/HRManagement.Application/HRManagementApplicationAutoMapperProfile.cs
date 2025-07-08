using HRManagement.Employees;
using System;
using HRManagement.Shared;
using Volo.Abp.Identity;
using Volo.Abp.AutoMapper;
using HRManagement.HRManagers;
using AutoMapper;

namespace HRManagement;

public class HRManagementApplicationAutoMapperProfile : Profile
{
    public HRManagementApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<HRManager, HRManagerDto>();
        CreateMap<HRManager, HRManagerExcelDto>();
        CreateMap<HRManagerWithNavigationProperties, HRManagerWithNavigationPropertiesDto>();
        CreateMap<IdentityUser, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Employee, EmployeeDto>();
        CreateMap<Employee, EmployeeExcelDto>();
        CreateMap<EmployeeWithNavigationProperties, EmployeeWithNavigationPropertiesDto>();
    }
}