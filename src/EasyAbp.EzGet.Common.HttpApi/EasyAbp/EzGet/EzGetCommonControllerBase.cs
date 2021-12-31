using EasyAbp.EzGet.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EzGet
{
    public abstract class EzGetCommonControllerBase : AbpControllerBase
    {
        protected EzGetCommonControllerBase()
        {
            LocalizationResource = typeof(EzGetResource);
        }
    }
}
