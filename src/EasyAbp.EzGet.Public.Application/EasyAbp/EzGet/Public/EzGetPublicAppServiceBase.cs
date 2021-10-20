using EasyAbp.EzGet.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Public
{
    public abstract class EzGetPublicAppServiceBase : ApplicationService
    {
        protected EzGetPublicAppServiceBase()
        {
            LocalizationResource = typeof(EzGetResource);
            ObjectMapperContext = typeof(EzGetPublicApplicationModule);
        }
    }
}
