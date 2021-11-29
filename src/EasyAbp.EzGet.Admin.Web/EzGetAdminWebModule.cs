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

namespace EasyAbp.EzGet.Admin.Web
{
    [DependsOn(
        typeof(EzGetCommonWebModule),
        typeof(EzGetAdminHttpApiModule)
        )]
    public class EzGetAdminWebModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new EzGetAdminMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EzGetAdminWebModule>();
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
            });
        }
    }
}
