using EasyAbp.EzGet.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EzGet.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class EzGetCommonPageModel : AbpPageModel
    {
        protected EzGetCommonPageModel()
        {
            LocalizationResourceType = typeof(EzGetResource);
            ObjectMapperContext = typeof(EzGetCommonWebModule);
        }
    }
}