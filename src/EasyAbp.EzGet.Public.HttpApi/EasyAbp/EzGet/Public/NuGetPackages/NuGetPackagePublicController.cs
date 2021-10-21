using EasyAbp.EzGet.NuGetPackages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace EasyAbp.EzGet.Public.NuGetPackages
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

        [HttpPost]
        public virtual Task<NuGetPackageDto> CreateAsync(CreateNuGetPackageInputWithStream input)
        {
            return NuGetPackagePublicAppService.CreateAsync(input);
        }
    }
}
