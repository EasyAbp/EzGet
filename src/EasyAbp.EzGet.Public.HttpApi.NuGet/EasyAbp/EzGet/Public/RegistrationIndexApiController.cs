﻿using EasyAbp.EzGet.AspNetCore.Authentication;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet.RegistrationIndexs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public virtual async Task<IActionResult> GetIndexAsync(string id)
        {
            return new JsonResult(await _registrationIndexAppService.GetIndexAsync(id));
        }

        [HttpGet]
        [Route("{id}/{version}.json")]
        public virtual async Task<IActionResult> GetLeafAsync(string id, string version)
        {
            return new JsonResult(await _registrationIndexAppService.GetLeafAsync(id, version));
        }
    }
}
