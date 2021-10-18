using EasyAbp.EzGet.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(EzGetEntityFrameworkCoreTestModule)
        )]
    public class EzGetDomainTestModule : AbpModule
    {
        
    }
}
