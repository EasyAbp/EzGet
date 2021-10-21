using EasyAbp.EzGet.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EzGet
{
    public abstract class EzGetCommonControllerBase : AbpController
    {
        protected EzGetCommonControllerBase()
        {
            LocalizationResource = typeof(EzGetResource);
        }
    }
}
