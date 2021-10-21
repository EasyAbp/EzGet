using EasyAbp.EzGet.Public.NuGetPackages;
using System;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Public
{
    [DependsOn(
        typeof(EzGetCommonHttpApiModule),
        typeof(EzGetPublicApplicationContractsModule))]
    public class EzGetPublicHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(CreateNuGetPackageInputWithStream));
            });
        }
    }
}
