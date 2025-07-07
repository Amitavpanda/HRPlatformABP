using HRManagement.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace HRManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(HRManagementEntityFrameworkCoreModule),
    typeof(HRManagementApplicationContractsModule)
)]
public class HRManagementDbMigratorModule : AbpModule
{
}
