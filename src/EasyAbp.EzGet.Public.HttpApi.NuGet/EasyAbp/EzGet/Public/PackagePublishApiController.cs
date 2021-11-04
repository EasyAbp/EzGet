using EasyAbp.EzGet.AspNetCore.Authentication;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet.Packages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/package-publish-resource
    [Route(ServiceIndexUrlConsts.PackagePublishUrl)]
    [Authorize(AuthenticationSchemes = EzGetAspNetCoreAuthenticationConsts.EzGetCredentialAuthenticationScheme)]
    public class PackagePublishApiController : EzGetHttpApiNuGetControllerBase
    {
        private readonly INuGetPackagePublicAppService _nuGetPackagePublicAppService;

        public PackagePublishApiController(INuGetPackagePublicAppService nuGetPackagePublicAppService)
        {
            _nuGetPackagePublicAppService = nuGetPackagePublicAppService;
        }

        [HttpPost]
        public virtual Task CreateAsync(CreateNuGetPackageInputWithStream input)
        {
            return _nuGetPackagePublicAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}/{version}")]
        public virtual async Task<IActionResult> UnlistAsync(string id, string version)
        {
            var package = await _nuGetPackagePublicAppService.GetAsync(id, version);

            if (null == package)
            {
                return NotFound();
            }

            await _nuGetPackagePublicAppService.UnlistAsync(id, version);
            return NoContent();
        }

        [HttpPost]
        [Route("{id}/{version}")]
        public virtual async Task<IActionResult> RelistAsync(string id, string version)
        {
            var package = await _nuGetPackagePublicAppService.GetAsync(id, version);

            if (null == package)
            {
                return NotFound();
            }

            await _nuGetPackagePublicAppService.RelistAsync(id, version);
            return NoContent();
        }
    }
}
