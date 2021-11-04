using EasyAbp.EzGet.AspNetCore.Authentication;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet.RegistrationIndexs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource
    [Route(ServiceIndexUrlConsts.RegistrationsBaseUrlUrl)]
    [Authorize(AuthenticationSchemes = EzGetAspNetCoreAuthenticationConsts.EzGetBasicAuthenticationScheme)]
    public class RegistrationIndexApiController : EzGetHttpApiNuGetControllerBase
    {
        private readonly IRegistrationIndexAppService _registrationIndexAppService;

        public RegistrationIndexApiController(IRegistrationIndexAppService registrationIndexAppService)
        {
            _registrationIndexAppService = registrationIndexAppService;
        }

        [HttpGet]
        [Route("{id}/index.json")]
        public virtual async Task<IActionResult> GetAsync(string id)
        {
            return new JsonResult(await _registrationIndexAppService.GetAsync(id));
        }
    }
}
