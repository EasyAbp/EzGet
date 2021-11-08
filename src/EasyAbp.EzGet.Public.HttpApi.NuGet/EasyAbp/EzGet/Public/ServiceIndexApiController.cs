﻿using EasyAbp.EzGet.AspNetCore.Authentication;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet.ServiceIndexs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/service-index
    [Authorize(AuthenticationSchemes = EzGetAspNetCoreAuthenticationConsts.EzGetBasicAuthenticationScheme)]
    public class ServiceIndexApiController : EzGetHttpApiNuGetControllerBase
    {
        protected IServiceIndexAppService ServiceIndexAppService { get; }

        public ServiceIndexApiController(IServiceIndexAppService serviceIndexAppService)
        {
            ServiceIndexAppService = serviceIndexAppService;
        }

        public virtual async Task<IActionResult> GetAsync()
        {
            var serviceIndex = await ServiceIndexAppService.GetAsync();
            return new JsonResult(serviceIndex);
        }
    }
}
