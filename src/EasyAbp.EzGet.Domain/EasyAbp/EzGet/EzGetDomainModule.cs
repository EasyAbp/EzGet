using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Users;

namespace EasyAbp.EzGet
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpUsersDomainModule),
        typeof(EzGetDomainSharedModule)
    )]
    public class EzGetDomainModule : AbpModule
    {

    }
}
