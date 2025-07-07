using Volo.Abp.Modularity;

namespace HRManagement;

/* Inherit from this class for your domain layer tests. */
public abstract class HRManagementDomainTestBase<TStartupModule> : HRManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
