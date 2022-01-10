using EasyAbp.EzGet.Admin.Web.Menus;
using EasyAbp.EzGet.Localization;
using EasyAbp.EzGet.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Localization;
using EasyAbp.EzGet.Admin.Permissions;
using Volo.Abp.AspNetCore.Mvc.Localization;

namespace EasyAbp.EzGet.Admin.Web
{
    [DependsOn(
        typeof(EzGetCommonWebModule),
        typeof(EzGetAdminHttpApiModule)
        )]
    public class EzGetAdminWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(EzGetResource),
                    typeof(EzGetAdminWebModule).Assembly,
                    typeof(EzGetAdminApplicationContractsModule).Assembly,
                    typeof(EzGetCommonApplicationContractsModule).Assembly
                );
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EzGetAdminWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new EzGetAdminMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EzGetAdminWebModule>("EasyAbp.EzGet.Admin.Web");
            });

            context.Services.AddAutoMapperObjectMapper<EzGetAdminWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EzGetAdminWebModule>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                //Configure authorization.
            });

            Configure<AbpPageToolbarOptions>(options =>
            {
                options.Configure<Pages.EzGet.Credentials.IndexModel>(
                        toolbar =>
                        {
                            toolbar.AddButton(
                                LocalizableString.Create<EzGetResource>("NewCredential"),
                                icon: "plus",
                                name: "CreateCredential",
                                requiredPolicyName: EzGetAdminPermissions.Credentials.Create
                            );
                        }
                    );

                options.Configure<Pages.EzGet.Feeds.IndexModel>(
                        toolbar =>
                        {
                            toolbar.AddButton(
                                LocalizableString.Create<EzGetResource>("NewFeed"),
                                icon: "plus",
                                name: "CreateFeed",
                                requiredPolicyName: EzGetAdminPermissions.Feeds.Create
                            );
                        }
                    );

                options.Configure<Pages.EzGet.NuGet.Packages.IndexModel>(
                        toolbar =>
                        {
                            toolbar.AddButton(
                                LocalizableString.Create<EzGetResource>("NewNuGetPackage"),
                                icon: "plus",
                                name: "CreateNuGetPackage",
                                requiredPolicyName: EzGetAdminPermissions.NuGetPackages.Create
                            );
                        }
                    );
            });
        }
    }
}
