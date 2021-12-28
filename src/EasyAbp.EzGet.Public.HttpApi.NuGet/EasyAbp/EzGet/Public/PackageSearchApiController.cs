using EasyAbp.EzGet.AspNetCore.Authentication;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.Models;
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
        private readonly IServiceIndexUrlGenerator _serviceIndexUrlGenerator;

        public PackageSearchApiController(
            INuGetPackagePublicAppService nuGetPackagePublicAppService,
            IServiceIndexUrlGenerator serviceIndexUrlGenerator)
        {
            _nuGetPackagePublicAppService = nuGetPackagePublicAppService;
            _serviceIndexUrlGenerator = serviceIndexUrlGenerator;
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
            var searchResult = await _nuGetPackagePublicAppService.SearchPackageListAsync(new SearchPackageListInput
            {
                Filter = query,
                SkipCount = skip,
                MaxResultCount = take,
                IncludePrerelease = prerelease,
                IncludeSemVer2 = semVerLevel == "2.0.0",
                PackageType = packageType,
                FeedName = feedName
            });

            return new JsonResult(new PackageSearchModel
            {
                Count = searchResult.Count,
                Packages = searchResult.Packages,
                Context = PackageSearchModel.SearchContext.Default(await _serviceIndexUrlGenerator.GetRegistrationsBaseUrlResourceUrlAsync(feedName))
            });
        }
    }
}
