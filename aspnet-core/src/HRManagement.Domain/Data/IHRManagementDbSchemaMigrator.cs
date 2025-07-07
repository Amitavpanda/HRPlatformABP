using System.Threading.Tasks;

namespace HRManagement.Data;

public interface IHRManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
