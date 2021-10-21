using EasyAbp.EzGet.NuGetPackages;
using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGetPackages
{
    public class NuGetPackagePublicAppService : EzGetPublicAppServiceBase, INuGetPackagePublicAppService
    {
        protected INuGetPackageManager NuGetPackageManager { get; }
        protected INuGetPackageRepository NuGetPackageRepository { get; }
        protected INuGetPackageAuthorizationService NuGetPackageAuthorizationService { get; }

        public NuGetPackagePublicAppService(
            INuGetPackageManager nuGetPackageManager,
            INuGetPackageRepository nuGetPackageRepository,
            INuGetPackageAuthorizationService nuGetPackageAuthorizationService)
        {
            NuGetPackageManager = nuGetPackageManager;
            NuGetPackageRepository = nuGetPackageRepository;
            NuGetPackageAuthorizationService = nuGetPackageAuthorizationService;
        }

        public virtual async Task<NuGetPackageDto> CreateAsync(CreateNuGetPackageInputWithStream input)
        {
            await NuGetPackageAuthorizationService.CheckCreationAsync();

            using (var stream = input.File.GetStream())
            {
                using (var packageReader = new PackageArchiveReader(stream, leaveStreamOpen: true))
                {
                    Stream readmeStream = null;
                    Stream iconStream = null;

                    var nuspecStream = await packageReader.GetNuspecAsync(default);
                    var package = await NuGetPackageManager.CreateAsync(packageReader);

                    if (package.HasReadme)
                    {
                        readmeStream = await packageReader.GetReadmeAsync();
                    }

                    if (package.HasEmbeddedIcon)
                    {
                        iconStream = await packageReader.GetIconAsync();
                    }

                    //TODO: Save stream to blob storing

                    return ObjectMapper.Map<NuGetPackage, NuGetPackageDto>(await NuGetPackageRepository.InsertAsync(package));
                }
            }
        }
    }
}
