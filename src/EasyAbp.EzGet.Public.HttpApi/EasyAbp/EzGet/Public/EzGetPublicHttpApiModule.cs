using EasyAbp.EzGet.Localization;
using EasyAbp.EzGet.Public.NuGet.Packages;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Public
{
    [DependsOn(
        typeof(EzGetCommonHttpApiModule),
        typeof(EzGetPublicApplicationContractsModule))]
    public class EzGetPublicHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EzGetPublicHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(CreateNuGetPackageInputWithStream));
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<EzGetResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
