using EasyAbp.EzGet.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EzGet.Public
{
    [ServiceFilter(typeof(EzGetHttpApiNuGetExceptionAttribute))]
    public abstract class EzGetHttpApiNuGetControllerBase : AbpController
    {
    }
}
