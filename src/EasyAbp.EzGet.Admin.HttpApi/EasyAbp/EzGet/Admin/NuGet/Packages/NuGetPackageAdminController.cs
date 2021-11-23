using EasyAbp.EzGet.NuGet.Packages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Admin.NuGet.Packages
{
    [RemoteService(Name = EzGetAdminRemoteServiceConsts.RemoteServiceName)]
    [Area("ezget-admin")]
    [Route("api/ezget-admin/nuget-packages")]
    public class NuGetPackageAdminController : EzGetAdminControllerBase, INuGetPackageAdminAppService
    {
        protected INuGetPackageAdminAppService NuGetPackageAdminAppService { get; }

        public NuGetPackageAdminController(INuGetPackageAdminAppService nuGetPackageAdminAppService)
        {
            NuGetPackageAdminAppService = nuGetPackageAdminAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<NuGetPackageDto> GetAsync(Guid id)
        {
            return NuGetPackageAdminAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("{packageName}/{version}")]
        public virtual Task<NuGetPackageDto> GetAsync(string packageName, string version, [FromQuery] string feedName)
        {
            return NuGetPackageAdminAppService.GetAsync(packageName, version, feedName);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<NuGetPackageDto>> GetListAsync(GetNuGetPackagesInput input)
        {
            return NuGetPackageAdminAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}/package-content")]
        public virtual Task<byte[]> GetPackageContentAsync(Guid id)
        {
            return NuGetPackageAdminAppService.GetPackageContentAsync(id);
        }

        [HttpGet]
        [Route("{id}/package-manifest")]
        public virtual Task<byte[]> GetPackageManifestAsync(Guid id)
        {
            return NuGetPackageAdminAppService.GetPackageManifestAsync(id);
        }

        [HttpPost]
        [Route("relist/{id}")]
        public virtual Task RelistAsync(Guid id)
        {
            return NuGetPackageAdminAppService.RelistAsync(id);
        }

        [HttpPost]
        [Route("unlist/{id}")]
        public virtual Task UnlistAsync(Guid id)
        {
            return NuGetPackageAdminAppService.UnlistAsync(id);
        }
    }
}
