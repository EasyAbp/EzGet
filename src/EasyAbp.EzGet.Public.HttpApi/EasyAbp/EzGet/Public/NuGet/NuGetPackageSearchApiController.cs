using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet.Packages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource
    [Route(ServiceIndexUrlConsts.SearchQueryServiceUrl)]
    public class NuGetPackageSearchApiController : ControllerBase
    {
        private readonly INuGetPackagePublicAppService _nuGetPackagePublicAppService;

        public NuGetPackageSearchApiController(INuGetPackagePublicAppService nuGetPackagePublicAppService)
        {
            _nuGetPackagePublicAppService = nuGetPackagePublicAppService;
        }

        [HttpGet]
        public virtual Task<NuGetPackageSearchListResultDto> GetAsync(
            [FromQuery(Name = "q")] string query,
            [FromQuery] int skip,
            [FromQuery] int take,
            [FromQuery] bool prerelease,
            [FromQuery] string semVerLevel,
            [FromQuery] string packageType)
        {
            return _nuGetPackagePublicAppService.SearchListAsync(new SearcherListInput
            {
                Filter = query,
                SkipCount = skip,
                MaxResultCount = take,
                IncludePrerelease = prerelease,
                IncludeSemVer2 = semVerLevel == "2.0.0",
                PackageType = packageType
            });
        }
    }
}
