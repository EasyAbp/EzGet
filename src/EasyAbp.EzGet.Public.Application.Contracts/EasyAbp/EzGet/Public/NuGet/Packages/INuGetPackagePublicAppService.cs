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
        Task<NuGetPackageDto> GetAsync(string packageName, string version, string feedName);
        Task<byte[]> GetPackageContentAsync(string packageName, string version, string feedName);
        Task<byte[]> GetPackageManifestAsync(string packageName, string version, string feedName);
        Task<List<string>> GetVersionListByPackageName(string packageName, string feedName);
        Task<NuGetPackageDto> CreateAsync(CreateNuGetPackageInputWithStream input, string feedName);
        Task UnlistAsync(string packageName, string version, string feedName);
        Task RelistAsync(string packageName, string version, string feedName);
    }
}
