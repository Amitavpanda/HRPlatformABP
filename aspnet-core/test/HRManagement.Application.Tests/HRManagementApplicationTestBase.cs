using Volo.Abp.Modularity;

namespace HRManagement;

public abstract class HRManagementApplicationTestBase<TStartupModule> : HRManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
