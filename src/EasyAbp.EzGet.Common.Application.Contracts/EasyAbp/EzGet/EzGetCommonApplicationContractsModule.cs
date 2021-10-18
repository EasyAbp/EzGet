using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.EzGet
{
    [DependsOn(
        typeof(EzGetDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class EzGetCommonApplicationContractsModule : AbpModule
    {

    }
}
