using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EzGet.Public
{
    [ServiceFilter(typeof(EzGetHttpApiNuGetExceptionAttribute))]
    public abstract class EzGetHttpApiNuGetControllerBase : AbpController
    {
    }
}
