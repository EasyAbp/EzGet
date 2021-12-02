using EasyAbp.EzGet.Admin.Permissions;
using EasyAbp.EzGet.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EzGet.Admin.Web.Menus
{
    public class EzGetAdminMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<EzGetResource>();

            var ezGetAdminMenus = new List<ApplicationMenuItem>();

            ezGetAdminMenus.Add(new ApplicationMenuItem(
                    EzGetAdminMenus.NuGetPackages.NuGetPackagesMenu,
                    l["NuGetPackages"].Value,
                    "/EzGet/NuGetPackages")
                .RequirePermissions(EzGetAdminPermissions.NuGetPackages.Default));

            ezGetAdminMenus.Add(new ApplicationMenuItem(
                    EzGetAdminMenus.Credentials.CredentialsMenu,
                    l["Credentials"].Value,
                    "/EzGet/Credentials")
                .RequirePermissions(EzGetAdminPermissions.Credentials.Default));

            ezGetAdminMenus.Add(new ApplicationMenuItem(
                    EzGetAdminMenus.Feeds.FeedsMenu,
                    l["Feeds"].Value,
                    "/EzGet/Feeds")
                .RequirePermissions(EzGetAdminPermissions.Feeds.Default));

            if (ezGetAdminMenus.Any())
            {
                var ezGetAdminMenu = context.Menu.FindMenuItem(EzGetAdminMenus.GroupName);

                if (ezGetAdminMenu == null)
                {
                    ezGetAdminMenu = new ApplicationMenuItem(
                        EzGetAdminMenus.GroupName,
                        l["EzGet"],
                        icon: "fa fa-globe");

                    context.Menu.AddItem(ezGetAdminMenu);
                }

                foreach (var menu in ezGetAdminMenus)
                {
                    ezGetAdminMenu.AddItem(menu);
                }
            }

            return Task.CompletedTask;
        }
    }
}