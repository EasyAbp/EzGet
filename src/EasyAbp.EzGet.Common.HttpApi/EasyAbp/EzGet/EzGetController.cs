using EasyAbp.EzGet.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EzGet
{
    public abstract class EzGetController : AbpController
    {
        protected EzGetController()
        {
            LocalizationResource = typeof(EzGetResource);
        }
    }
}
