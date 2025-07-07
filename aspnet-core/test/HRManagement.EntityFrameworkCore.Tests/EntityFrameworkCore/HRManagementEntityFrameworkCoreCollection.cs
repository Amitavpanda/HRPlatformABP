using Xunit;

namespace HRManagement.EntityFrameworkCore;

[CollectionDefinition(HRManagementTestConsts.CollectionDefinitionName)]
public class HRManagementEntityFrameworkCoreCollection : ICollectionFixture<HRManagementEntityFrameworkCoreFixture>
{

}
