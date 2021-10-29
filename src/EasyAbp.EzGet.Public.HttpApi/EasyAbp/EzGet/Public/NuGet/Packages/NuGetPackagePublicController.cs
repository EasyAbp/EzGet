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
        public Task<NuGetPackageDto> GetAsync(string packageName, string version)
        {
            return NuGetPackagePublicAppService.GetAsync(packageName, version);
        }

        [HttpGet]
        [Route("{packageName}/{version}/package-content")]
        public Task<byte[]> GetPackageContentAsync(string packageName, string version)
        {
            return NuGetPackagePublicAppService.GetPackageContentAsync(packageName, version);
        }

        [HttpGet]
        [Route("{packageName}/{version}/package-manifest")]
        public Task<byte[]> GetPackageManifestAsync(string packageName, string version)
        {
            return NuGetPackagePublicAppService.GetPackageManifestAsync(packageName, version);
        }

        [HttpGet]
        [Route("{packageName}/versions")]
        public Task<List<string>> GetVersionListByPackageName(string packageName)
        {
            return NuGetPackagePublicAppService.GetVersionListByPackageName(packageName);
        }

        [HttpPost]
        public virtual Task<NuGetPackageDto> CreateAsync(CreateNuGetPackageInputWithStream input)
        {
            return NuGetPackagePublicAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{packageName}/{version}")]
        public Task UnlistAsync(string packageName, string version)
        {
            return NuGetPackagePublicAppService.UnlistAsync(packageName, version);
        }

        [HttpPost]
        [Route("{packageName}/{version}")]
        public Task RelistAsync(string packageName, string version)
        {
            return NuGetPackagePublicAppService.RelistAsync(packageName, version);
        }

        [HttpGet]
        [Route("search")]
        public Task<NuGetPackageSearchListResultDto> SearchListAsync(SearcherListInput input)
        {
            return NuGetPackagePublicAppService.SearchListAsync(input);
        }
    }
}
