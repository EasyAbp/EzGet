using EasyAbp.EzGet.AspNetCore.Authentication;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Public
{
    [DependsOn(
        typeof(EzGetAspNetCoreAuthenticationModule),
        typeof(EzGetPublicApplicationModule))]
    public class EzGetPublicNuGetApiModule : AbpModule
    {
    }
}
