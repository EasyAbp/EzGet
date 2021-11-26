using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EzGet.Web.Menus
{
    public class EzGetCommonMenuContributor : IMenuContributor
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
            //Add main menu items.
            //context.Menu.AddItem(new ApplicationMenuItem(EzGetCommonMenus.GroupName, displayName: "EzGet", "~/EzGet", icon: "fa fa-globe"));

            return Task.CompletedTask;
        }
    }
}