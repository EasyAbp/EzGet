using EasyAbp.EzGet.NuGet.ServiceIndexs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.EzGet.Public.NuGet.Models;
using EasyAbp.EzGet.Public.NuGet.Packages;
using Volo.Abp.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EasyAbp.EzGet.AspNetCore.Authentication;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/package-base-address-resource
    [Route(ServiceIndexUrlConsts.PackageBaseAddressUrl)]
    [Authorize(AuthenticationSchemes = EzGetAspNetCoreAuthenticationConsts.EzGetBasicAuthenticationScheme)]
    public class PackageContentApiController : EzGetHttpApiNuGetControllerBase
    {
        protected INuGetPackagePublicAppService NuGetPackagePublicAppService { get; }

        public PackageContentApiController(INuGetPackagePublicAppService nuGetPackagePublicAppService)
        {
            NuGetPackagePublicAppService = nuGetPackagePublicAppService;
        }

        [HttpGet]
        [Route("{id}/index.json")]
        public virtual async Task<IActionResult> GetVersionsAsync(string id)
        {
            var versionList = await NuGetPackagePublicAppService.GetVersionListByPackageName(id);
            return new JsonResult(new VersionsModel { Versions = versionList });
        }

        [HttpGet]
        [Route("{id}/{version}/{idDotVersion}.nupkg")]
        public virtual async Task<IActionResult> GetPackageContentAsync(string id, string version)
        {
            var content = await NuGetPackagePublicAppService.GetPackageContentAsync(id, version);

            if(content == null)
                return NotFound();

            return File(content, "application/octet-stream");
        }

        [HttpGet]
        [Route("{id}/{version}/{idDotVersion}.nuspec")]
        public virtual async Task<IActionResult> GetPackageManifestAsync(string id, string version)
        {
            var manifest = await NuGetPackagePublicAppService.GetPackageManifestAsync(id, version);

            if (manifest == null)
                return NotFound();

            return File(manifest, "text/xml");
        }
    }
}
