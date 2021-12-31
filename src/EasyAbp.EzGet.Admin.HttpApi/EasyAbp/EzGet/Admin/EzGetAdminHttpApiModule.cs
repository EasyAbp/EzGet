using EasyAbp.EzGet.Admin.NuGet.Packages;
using EasyAbp.EzGet.Localization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Admin
{
    [DependsOn(
        typeof(EzGetCommonHttpApiModule),
        typeof(EzGetAdminApplicationContractsModule)
        )]
    public class EzGetAdminHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EzGetAdminHttpApiModule).Assembly);
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
