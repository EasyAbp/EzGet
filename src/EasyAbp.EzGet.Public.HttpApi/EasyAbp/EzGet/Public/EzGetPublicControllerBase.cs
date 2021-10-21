using EasyAbp.EzGet.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EzGet.Public
{
    public abstract class EzGetPublicControllerBase : AbpController
    {
        protected EzGetPublicControllerBase()
        {
            LocalizationResource = typeof(EzGetResource);
        }
    }
}
