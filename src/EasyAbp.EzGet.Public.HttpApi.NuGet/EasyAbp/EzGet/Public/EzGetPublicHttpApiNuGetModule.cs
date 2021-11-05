using EasyAbp.EzGet.AspNetCore.Authentication;
using Volo.Abp.Modularity;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EzGet.Public
{
    [DependsOn(
        typeof(EzGetAspNetCoreAuthenticationModule),
        typeof(EzGetPublicApplicationModule),
        typeof(AbpAspNetCoreMvcModule)
        )]
    public class EzGetPublicHttpApiNuGetModule : AbpModule
    {
        //TODO: Add Exception filter, handle AbpValidationException, AbpAuthorizationException, EntityNotFoundException, IBusinessException
    }
}
