using EasyAbp.EzGet.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EzGet.Admin
{
    public abstract class EzGetAdminControllerBase : AbpController
    {
        protected EzGetAdminControllerBase()
        {
            LocalizationResource = typeof(EzGetResource);
        }
    }
}
