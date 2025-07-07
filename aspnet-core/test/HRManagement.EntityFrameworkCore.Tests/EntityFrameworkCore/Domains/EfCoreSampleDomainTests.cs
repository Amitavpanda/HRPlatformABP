using HRManagement.Samples;
using Xunit;

namespace HRManagement.EntityFrameworkCore.Domains;

[Collection(HRManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<HRManagementEntityFrameworkCoreTestModule>
{

}
