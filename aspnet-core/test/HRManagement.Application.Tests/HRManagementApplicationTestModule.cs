using Volo.Abp.Modularity;

namespace HRManagement;

[DependsOn(
    typeof(HRManagementApplicationModule),
    typeof(HRManagementDomainTestModule)
)]
public class HRManagementApplicationTestModule : AbpModule
{

}
