using EasyAbp.EzGet.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EzGet.Pages
{
    public abstract class EzGetPageModel : AbpPageModel
    {
        protected EzGetPageModel()
        {
            LocalizationResourceType = typeof(EzGetResource);
        }
    }
}