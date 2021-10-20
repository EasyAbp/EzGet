using EasyAbp.EzGet.NuGetPackages;
using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGetPackages
{
    public class NuGetPackagePublicAppService : EzGetPublicAppServiceBase
    {
        protected INuGetPackageManager NuGetPackageManager { get; }
        protected INuGetPackageRepository NuGetPackageRepository { get; }

        public NuGetPackagePublicAppService(
            INuGetPackageManager nuGetPackageManager,
            INuGetPackageRepository nuGetPackageRepository)
        {
            NuGetPackageManager = nuGetPackageManager;
            NuGetPackageRepository = nuGetPackageRepository;
        }

        public virtual async Task CreateAsync(CreateNuGetPackageDto input)
        {
            //Todo Verify user's permission or apiKey in http head of nuget api controller

            NuGetPackage package;
            Stream nuspecStream = null;
            Stream readmeStream = null;
            Stream iconStream = null;

            using (var stream = input.File.GetStream())
            {
                using (var packageReader = new PackageArchiveReader(stream, leaveStreamOpen: true))
                {
                    package = await NuGetPackageManager.CreateAsync(packageReader);
                    nuspecStream = await packageReader.GetNuspecAsync(default);

                    if (package.HasReadme)
                    {
                        readmeStream = await packageReader.GetReadmeAsync();
                    }

                    if (package.HasEmbeddedIcon)
                    {
                        iconStream = await packageReader.GetIconAsync();
                    }
                }
            }

            //Todo Save stream to blob storing

            ObjectMapper.Map<NuGetPackage, NuGetPackageDto>(await NuGetPackageRepository.InsertAsync(package));
        }
    }
}
