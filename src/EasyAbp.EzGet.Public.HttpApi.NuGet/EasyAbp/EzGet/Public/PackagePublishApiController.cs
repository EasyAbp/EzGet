using EasyAbp.EzGet.AspNetCore.Authentication;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet.Packages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;

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

        [HttpPut]
        public virtual Task CreateAsync()
        {
            var input = new CreateNuGetPackageInputWithStream
            {
                File = new RemoteStreamContent(GetUploadStreamOrNull(HttpContext.Request))
            };
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

        private Stream GetUploadStreamOrNull(HttpRequest request)
        {
            Stream rawUploadStream;

            if (request.HasFormContentType && request.Form.Files.Count > 0)
            {
                rawUploadStream = request.Form.Files[0].OpenReadStream();
            }
            else
            {
                rawUploadStream = request.Body;
            }

            return rawUploadStream;
        }
    }
}
