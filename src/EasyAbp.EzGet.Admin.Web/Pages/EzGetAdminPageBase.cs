using EasyAbp.EzGet.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Layout;

namespace EasyAbp.EzGet.Admin.Web.Pages
{
    public class EzGetAdminPageBase : Page
    {
        [RazorInject] public IStringLocalizer<EzGetResource> L { get; set; }

        [RazorInject] public IPageLayout PageLayout { get; set; }

        public override Task ExecuteAsync()
        {
            return Task.CompletedTask; // Will be overriden by razor pages. (.cshtml)
        }
    }
}
