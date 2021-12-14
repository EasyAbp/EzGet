using EasyAbp.EzGet.AspNetCore.Authentication;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet.Packages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource
    [AllowAnonymous]
    [Authorize(AuthenticationSchemes = EzGetAspNetCoreAuthenticationConsts.EzGetBasicAuthenticationScheme)]
    public class PackageSearchApiController : EzGetHttpApiNuGetControllerBase
    {
        private readonly INuGetPackagePublicAppService _nuGetPackagePublicAppService;

        public PackageSearchApiController(INuGetPackagePublicAppService nuGetPackagePublicAppService)
        {
            _nuGetPackagePublicAppService = nuGetPackagePublicAppService;
        }

        public virtual async Task<IActionResult> GetAsync(
            [FromQuery(Name = "q")] string query = null,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 20,
            [FromQuery] bool prerelease = false,
            [FromQuery] string semVerLevel = null,
            [FromQuery] string packageType = null,
            [FromRoute] string feedName = null)
        {
            return new JsonResult(await _nuGetPackagePublicAppService.SearchListAsync(new SearcherListInput
            {
                Filter = query,
                SkipCount = skip,
                MaxResultCount = take,
                IncludePrerelease = prerelease,
                IncludeSemVer2 = semVerLevel == "2.0.0",
                PackageType = packageType,
                FeedName = feedName
            }));
        }
    }
}
