using Localization.Resources.AbpUi;
using EasyAbp.EzGet.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EzGet
{
    [DependsOn(
        typeof(EzGetCommonApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class EzGetCommonHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EzGetCommonHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<EzGetResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
