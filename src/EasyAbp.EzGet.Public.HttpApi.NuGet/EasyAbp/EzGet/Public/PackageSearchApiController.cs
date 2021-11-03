using EasyAbp.EzGet.AspNetCore.Authentication;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet.Packages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource
    [Route(ServiceIndexUrlConsts.SearchQueryServiceUrl)]
    [Authorize(AuthenticationSchemes = EzGetAspNetCoreAuthenticationConsts.EzGetBasicAuthenticationScheme)]
    public class PackageSearchApiController : AbpController
    {
        private readonly INuGetPackagePublicAppService _nuGetPackagePublicAppService;

        public PackageSearchApiController(INuGetPackagePublicAppService nuGetPackagePublicAppService)
        {
            _nuGetPackagePublicAppService = nuGetPackagePublicAppService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync(
            [FromQuery(Name = "q")] string query,
            [FromQuery] int skip,
            [FromQuery] int take,
            [FromQuery] bool prerelease,
            [FromQuery] string semVerLevel,
            [FromQuery] string packageType)
        {
            return new JsonResult(await _nuGetPackagePublicAppService.SearchListAsync(new SearcherListInput
            {
                Filter = query,
                SkipCount = skip,
                MaxResultCount = take,
                IncludePrerelease = prerelease,
                IncludeSemVer2 = semVerLevel == "2.0.0",
                PackageType = packageType
            }));
        }
    }
}
