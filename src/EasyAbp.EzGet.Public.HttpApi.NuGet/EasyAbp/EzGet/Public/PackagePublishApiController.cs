using EasyAbp.EzGet.AspNetCore.Authentication;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet.Packages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/package-publish-resource
    [Authorize(AuthenticationSchemes = EzGetAspNetCoreAuthenticationConsts.EzGetCredentialAuthenticationScheme)]
    public class PackagePublishApiController : EzGetHttpApiNuGetControllerBase
    {
        private readonly INuGetPackagePublicAppService _nuGetPackagePublicAppService;

        public PackagePublishApiController(INuGetPackagePublicAppService nuGetPackagePublicAppService)
        {
            _nuGetPackagePublicAppService = nuGetPackagePublicAppService;
        }

        public virtual Task CreateAsync(string feedName = null)
        {
            var input = new CreateNuGetPackageInputWithStream
            {
                File = new RemoteStreamContent(GetUploadStreamOrNull(HttpContext.Request))
            };
            return _nuGetPackagePublicAppService.CreateAsync(input, feedName);
        }

        public virtual async Task<IActionResult> UnlistAsync(string id, string version, string feedName = null)
        {
            var package = await _nuGetPackagePublicAppService.GetAsync(id, version, feedName);

            if (null == package)
            {
                return NotFound();
            }

            await _nuGetPackagePublicAppService.UnlistAsync(id, version, feedName);
            return NoContent();
        }

        public virtual async Task<IActionResult> RelistAsync(string id, string version, string feedName = null)
        {
            var package = await _nuGetPackagePublicAppService.GetAsync(id, version, feedName);

            if (null == package)
            {
                return NotFound();
            }

            await _nuGetPackagePublicAppService.RelistAsync(id, version, feedName);
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
