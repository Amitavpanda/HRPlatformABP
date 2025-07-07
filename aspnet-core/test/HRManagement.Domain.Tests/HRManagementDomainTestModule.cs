using Volo.Abp.Modularity;

namespace HRManagement;

[DependsOn(
    typeof(HRManagementDomainModule),
    typeof(HRManagementTestBaseModule)
)]
public class HRManagementDomainTestModule : AbpModule
{

}
