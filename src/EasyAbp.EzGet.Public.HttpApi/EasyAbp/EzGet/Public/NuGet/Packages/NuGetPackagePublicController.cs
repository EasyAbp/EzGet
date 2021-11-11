using EasyAbp.EzGet.NuGet.Packages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace EasyAbp.EzGet.Public.NuGet.Packages
{
    [RemoteService(Name = EzGetPublicRemoteServiceConsts.RemoteServiceName)]
    [Area("ezget-public")]
    [Route("api/ezget-public/nuget-packages")]
    public class NuGetPackagePublicController : EzGetPublicControllerBase, INuGetPackagePublicAppService
    {
        protected INuGetPackagePublicAppService NuGetPackagePublicAppService { get; }

        public NuGetPackagePublicController(INuGetPackagePublicAppService nuGetPackagePublicAppService)
        {
            NuGetPackagePublicAppService = nuGetPackagePublicAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<NuGetPackageDto> GetAsync(Guid id)
        {
            return NuGetPackagePublicAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("{packageName}/{version}")]
        public Task<NuGetPackageDto> GetAsync(string packageName, string version, [FromQuery] string feedName = null)
        {
            return NuGetPackagePublicAppService.GetAsync(packageName, version, feedName);
        }

        [HttpGet]
        [Route("{packageName}/{version}/package-content")]
        public Task<byte[]> GetPackageContentAsync(string packageName, string version, [FromQuery] string feedName = null)
        {
            return NuGetPackagePublicAppService.GetPackageContentAsync(packageName, version, feedName);
        }

        [HttpGet]
        [Route("{packageName}/{version}/package-manifest")]
        public Task<byte[]> GetPackageManifestAsync(string packageName, string version, [FromQuery] string feedName = null)
        {
            return NuGetPackagePublicAppService.GetPackageManifestAsync(packageName, version, feedName);
        }

        [HttpGet]
        [Route("{packageName}/versions")]
        public Task<List<string>> GetVersionListByPackageName(string packageName, [FromQuery] string feedName = null)
        {
            return NuGetPackagePublicAppService.GetVersionListByPackageName(packageName, feedName);
        }

        [HttpPost]
        public virtual Task<NuGetPackageDto> CreateAsync(CreateNuGetPackageInputWithStream input, [FromQuery] string feedName = null)
        {
            return NuGetPackagePublicAppService.CreateAsync(input, feedName);
        }

        [HttpDelete]
        [Route("{packageName}/{version}")]
        public Task UnlistAsync(string packageName, string version, [FromQuery] string feedName = null)
        {
            return NuGetPackagePublicAppService.UnlistAsync(packageName, version, feedName);
        }

        [HttpPost]
        [Route("{packageName}/{version}")]
        public Task RelistAsync(string packageName, string version, [FromQuery] string feedName = null)
        {
            return NuGetPackagePublicAppService.RelistAsync(packageName, version, feedName);
        }

        [HttpGet]
        [Route("search")]
        public Task<NuGetPackageSearchListResultDto> SearchListAsync(SearcherListInput input)
        {
            return NuGetPackagePublicAppService.SearchListAsync(input);
        }
    }
}
