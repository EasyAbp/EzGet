using EasyAbp.EzGet.NuGet.Packages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Public.NuGet.Packages
{
    public interface INuGetPackagePublicAppService : IApplicationService
    {
        Task<NuGetPackageSearchListResultDto> SearchListAsync(SearcherListInput input);
        Task<NuGetPackageDto> GetAsync(Guid id);
        Task<NuGetPackageDto> GetAsync(string packageName, string version);
        Task<byte[]> GetPackageContentAsync(string packageName, string version);
        Task<byte[]> GetPackageManifestAsync(string packageName, string version);
        Task<List<string>> GetVersionListByPackageName(string packageName);
        Task<NuGetPackageDto> CreateAsync(CreateNuGetPackageInputWithStream input);
        Task UnlistAsync(string packageName, string version);
        Task RelistAsync(string packageName, string version);
    }
}
