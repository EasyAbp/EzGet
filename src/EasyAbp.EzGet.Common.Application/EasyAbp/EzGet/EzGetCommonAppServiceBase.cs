using EasyAbp.EzGet.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet
{
    public abstract class EzGetCommonAppServiceBase : ApplicationService
    {
        protected EzGetCommonAppServiceBase()
        {
            LocalizationResource = typeof(EzGetResource);
            ObjectMapperContext = typeof(EzGetCommonApplicationModule);
        }
    }
}
