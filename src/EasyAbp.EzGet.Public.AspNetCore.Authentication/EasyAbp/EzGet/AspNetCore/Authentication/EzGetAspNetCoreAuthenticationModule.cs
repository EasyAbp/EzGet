using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.AspNetCore.Authentication
{
    [DependsOn(
        typeof(EzGetDomainModule)
        )]
    public class EzGetAspNetCoreAuthenticationModule : AbpModule
    {
    }
}
