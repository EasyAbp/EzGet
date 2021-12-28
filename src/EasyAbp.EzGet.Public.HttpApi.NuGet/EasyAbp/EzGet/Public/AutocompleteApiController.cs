using EasyAbp.EzGet.AspNetCore.Authentication;
using EasyAbp.EzGet.Public.Models;
using EasyAbp.EzGet.Public.NuGet.Packages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public
{
    //See: https://docs.microsoft.com/en-us/nuget/api/search-autocomplete-service-resource
    [AllowAnonymous]
    [Authorize(AuthenticationSchemes = EzGetAspNetCoreAuthenticationConsts.EzGetBasicAuthenticationScheme)]
    public class AutocompleteApiController : EzGetHttpApiNuGetControllerBase
    {
        protected INuGetPackagePublicAppService NuGetPackagePublicAppService { get; }

        public AutocompleteApiController(INuGetPackagePublicAppService nuGetPackagePublicAppService)
        {
            NuGetPackagePublicAppService = nuGetPackagePublicAppService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync(
            [FromQuery(Name = "q")] string query = null,
            [FromQuery(Name = "id")] string packageName = null,
            [FromQuery] bool prerelease = false,
            [FromQuery] string semVerLevel = null,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 20,
            [FromQuery] string packageType = null,
            [FromRoute] string feedName = null)
        {
            var includeSemVer2 = semVerLevel == "2.0.0";

            long count;
            IEnumerable<string> data;

            if (packageName != null && query == null)
            {
                var versionList = await NuGetPackagePublicAppService.GetVersionListAsync(new GetVersionListInput
                {
                    PackageName = packageName,
                    IncludePrerelease = prerelease,
                    IncludeSemVer2 = includeSemVer2,
                    FeedName = feedName
                });

                count = versionList.Count > 0 ? 1 : 0;
                data = versionList;
            }
            else
            {
                var nameListResult = await NuGetPackagePublicAppService.SearchNameListAsync(new SearchNameListInput
                {
                    Filter = query,
                    IncludePrerelease = prerelease,
                    IncludeSemVer2 = includeSemVer2,
                    SkipCount = skip,
                    MaxResultCount = take,
                    FeedName = feedName
                });

                count = nameListResult.Count;
                data = nameListResult.Names;
            }

            var result = new AutocompleteResultModel
            {
                Context = AutocompleteResultModel.AutocompleteContext.Default(),
                Count = count,
                Data = data
            };

            return new JsonResult(result);
        }
    }
}
