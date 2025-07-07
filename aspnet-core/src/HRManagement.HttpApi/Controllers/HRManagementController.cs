using HRManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HRManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HRManagementController : AbpControllerBase
{
    protected HRManagementController()
    {
        LocalizationResource = typeof(HRManagementResource);
    }
}
