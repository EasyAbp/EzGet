using EasyAbp.EzGet.NuGet.ServiceIndexs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.EzGet.Public.NuGet.Models;
using EasyAbp.EzGet.Public.NuGet.Packages;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/package-base-address-resource
    [Route(ServiceIndexUrlConsts.PackageBaseAddressUrl)]
    public class NuGetPackageContentApiController : ControllerBase
    {
        protected INuGetPackagePublicAppService NuGetPackagePublicAppService { get; }

        public NuGetPackageContentApiController(INuGetPackagePublicAppService nuGetPackagePublicAppService)
        {
            NuGetPackagePublicAppService = nuGetPackagePublicAppService;
        }

        [HttpGet]
        [Route("{id}/index.json")]
        public virtual async Task<VersionsModel> GetVersionsAsync(string id)
        {
            var versionList = await NuGetPackagePublicAppService.GetVersionListByPackageName(id);
            return new VersionsModel { Versions = versionList };
        }

        [HttpGet]
        [Route("{id}/{version}/{id}.{version}.nupkg")]
        public virtual async Task<IActionResult> GetPackageContentAsync(string id, string version)
        {
            var content = await NuGetPackagePublicAppService.GetPackageContentAsync(id, version);

            if(content == null)
                return NotFound();

            return File(content, "application/octet-stream");
        }

        [HttpGet]
        [Route("{id}/{version}/{id}.{version}.nuspec")]
        public virtual async Task<IActionResult> GetPackageManifestAsync(string id, string version)
        {
            var manifest = await NuGetPackagePublicAppService.GetPackageManifestAsync(id, version);

            if (manifest == null)
                return NotFound();

            return File(manifest, "text/xml");
        }
    }
}
