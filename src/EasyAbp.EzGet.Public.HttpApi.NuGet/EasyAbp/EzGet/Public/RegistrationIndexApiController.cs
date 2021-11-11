using EasyAbp.EzGet.AspNetCore.Authentication;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet.RegistrationIndexs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource
    [Authorize(AuthenticationSchemes = EzGetAspNetCoreAuthenticationConsts.EzGetBasicAuthenticationScheme)]
    public class RegistrationIndexApiController : EzGetHttpApiNuGetControllerBase
    {
        private readonly IRegistrationIndexAppService _registrationIndexAppService;

        public RegistrationIndexApiController(IRegistrationIndexAppService registrationIndexAppService)
        {
            _registrationIndexAppService = registrationIndexAppService;
        }

        public virtual async Task<IActionResult> GetIndexAsync(string id, string feedName = null)
        {
            return new JsonResult(await _registrationIndexAppService.GetIndexAsync(id, feedName));
        }

        public virtual async Task<IActionResult> GetLeafAsync(string id, string version, string feedName = null)
        {
            return new JsonResult(await _registrationIndexAppService.GetLeafAsync(id, version, feedName));
        }
    }
}
