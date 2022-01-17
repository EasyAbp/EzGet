using EasyAbp.EzGet.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EzGet.Public.Web.Pages
{
    public class EzGetPublicPageModel : AbpPageModel
    {
        public EzGetPublicPageModel()
        {
            LocalizationResourceType = typeof(EzGetResource);
            ObjectMapperContext = typeof(EzGetPublicWebModule);
        }
    }
}
