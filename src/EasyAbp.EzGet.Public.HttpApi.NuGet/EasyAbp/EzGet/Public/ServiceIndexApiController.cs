using EasyAbp.EzGet.AspNetCore.Authentication;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet.ServiceIndexs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/service-index
    [AllowAnonymous]
    [Authorize(AuthenticationSchemes = EzGetAspNetCoreAuthenticationConsts.EzGetBasicAuthenticationScheme)]
    public class ServiceIndexApiController : EzGetHttpApiNuGetControllerBase
    {
        protected IServiceIndexPublicAppService ServiceIndexAppService { get; }

        public ServiceIndexApiController(IServiceIndexPublicAppService serviceIndexAppService)
        {
            ServiceIndexAppService = serviceIndexAppService;
        }

        public virtual async Task<IActionResult> GetAsync(string feedName = null)
        {
            var serviceIndex = await ServiceIndexAppService.GetAsync(feedName);
            return new JsonResult(serviceIndex);
        }
    }
}
