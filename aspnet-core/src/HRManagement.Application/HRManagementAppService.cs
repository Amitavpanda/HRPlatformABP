using HRManagement.Localization;
using Volo.Abp.Application.Services;

namespace HRManagement;

/* Inherit your application services from this class.
 */
public abstract class HRManagementAppService : ApplicationService
{
    protected HRManagementAppService()
    {
        LocalizationResource = typeof(HRManagementResource);
    }
}
