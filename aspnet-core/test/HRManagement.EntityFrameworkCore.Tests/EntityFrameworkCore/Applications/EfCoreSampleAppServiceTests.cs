using HRManagement.Samples;
using Xunit;

namespace HRManagement.EntityFrameworkCore.Applications;

[Collection(HRManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<HRManagementEntityFrameworkCoreTestModule>
{

}
