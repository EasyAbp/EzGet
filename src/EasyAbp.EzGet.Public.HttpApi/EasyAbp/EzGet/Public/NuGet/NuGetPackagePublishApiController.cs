using EasyAbp.EzGet.Public.NuGet.Packages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/package-publish-resource
    [Route("api/v2/package")]
    public class NuGetPackagePublishApiController : ControllerBase
    {
        private readonly INuGetPackagePublicAppService _nuGetPackagePublicAppService;

        public NuGetPackagePublishApiController(INuGetPackagePublicAppService nuGetPackagePublicAppService)
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
