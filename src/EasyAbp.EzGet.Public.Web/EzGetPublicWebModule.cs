using EasyAbp.EzGet.Localization;
using EasyAbp.EzGet.Public.Web.Menus;
using EasyAbp.EzGet.Web;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EzGet.Public.Web
{
    [DependsOn(
        typeof(EzGetCommonWebModule),
        typeof(EzGetPublicHttpApiModule)
        )]
    public class EzGetPublicWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(EzGetResource),
                    typeof(EzGetPublicWebModule).Assembly,
                    typeof(EzGetPublicApplicationContractsModule).Assembly,
                    typeof(EzGetCommonApplicationContractsModule).Assembly
                );
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EzGetPublicWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new EzGetPublicMenuContributor());
                options.MainMenuNames.Add(EzGetMenus.Public);
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EzGetPublicWebModule>("EasyAbp.EzGet.Public.Web");
            });

            context.Services.AddAutoMapperObjectMapper<EzGetPublicWebModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EzGetPublicWebModule>(validate: true);
            });
        }
    }
}
