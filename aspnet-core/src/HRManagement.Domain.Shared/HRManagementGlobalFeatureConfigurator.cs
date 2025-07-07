using Volo.Abp.GlobalFeatures;
using Volo.Abp.Threading;

namespace HRManagement;

public static class HRManagementGlobalFeatureConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
           /* You can configure (enable/disable) global features of the used modules here.
            * Please refer to the documentation to learn more about the Global Features System:
            * https://abp.io/docs/latest/framework/infrastructure/global-features
            */
        });
    }
}
