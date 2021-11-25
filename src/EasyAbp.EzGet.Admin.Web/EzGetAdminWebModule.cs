using EasyAbp.EzGet.Admin.Web.Menus;
using EasyAbp.EzGet.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

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
        }
    }
}
