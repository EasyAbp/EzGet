﻿using EasyAbp.EzGet.NuGetPackages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Public.NuGetPackages
{
    public interface INuGetPackagePublicAppService : IApplicationService
    {
        Task<NuGetPackageDto> GetAsync(Guid id);
        Task<NuGetPackageDto> GetAsync(string packageName, string version);
        Task<NuGetPackageDto> CreateAsync(CreateNuGetPackageInputWithStream input);
        Task UnlistAsync(string packageName, string version);
        Task RelistAsync(string packageName, string version);
    }
}
