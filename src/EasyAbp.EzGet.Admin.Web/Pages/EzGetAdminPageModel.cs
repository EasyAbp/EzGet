using EasyAbp.EzGet.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EzGet.Admin.Web.Pages
{
    public abstract class EzGetAdminPageModel : AbpPageModel
    {
        protected EzGetAdminPageModel()
        {
            LocalizationResourceType = typeof(EzGetResource);
            ObjectMapperContext = typeof(EzGetAdminWebModule);
        }
    }
}