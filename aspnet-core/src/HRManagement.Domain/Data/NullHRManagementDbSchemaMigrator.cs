using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HRManagement.Data;

/* This is used if database provider does't define
 * IHRManagementDbSchemaMigrator implementation.
 */
public class NullHRManagementDbSchemaMigrator : IHRManagementDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
