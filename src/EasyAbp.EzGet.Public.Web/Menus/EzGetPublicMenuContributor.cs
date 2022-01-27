using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EzGet.Public.Web.Menus
{
    public class EzGetPublicMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == EzGetMenus.Public)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            return Task.CompletedTask;
        }
    }
}
