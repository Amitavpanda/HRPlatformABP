using Microsoft.Extensions.Localization;
using HRManagement.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace HRManagement;

[Dependency(ReplaceServices = true)]
public class HRManagementBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<HRManagementResource> _localizer;

    public HRManagementBrandingProvider(IStringLocalizer<HRManagementResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
