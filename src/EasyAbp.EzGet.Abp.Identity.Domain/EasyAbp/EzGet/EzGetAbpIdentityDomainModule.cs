using Volo.Abp.Modularity;
using Volo.Abp.Identity;

namespace EasyAbp.EzGet
{
    [DependsOn(
        typeof(EzGetDomainModule),
        typeof(AbpIdentityDomainModule)
        )]
    public class EzGetAbpIdentityDomainModule : AbpModule
    {
    }
}
