using EasyAbp.EzGet.NuGet.Packages;
using NuGet.Packaging;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Specifications;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.BlobStoring;
using EasyAbp.EzGet.NuGet;

namespace EasyAbp.EzGet.Public.NuGet.Packages
{
    public class NuGetPackagePublicAppService : EzGetPublicAppServiceBase, INuGetPackagePublicAppService
    {
        protected INuGetPackageManager NuGetPackageManager { get; }
        protected INuGetPackageRepository NuGetPackageRepository { get; }
        protected INuGetPackageAuthorizationService NuGetPackageAuthorizationService { get; }
        protected IBlobContainer<NuGetContainer> BlobContainer { get; }


        public NuGetPackagePublicAppService(
            INuGetPackageManager nuGetPackageManager,
            INuGetPackageRepository nuGetPackageRepository,
            INuGetPackageAuthorizationService nuGetPackageAuthorizationService,
            IBlobContainer<NuGetContainer> blobContainer)
        {
            NuGetPackageManager = nuGetPackageManager;
            NuGetPackageRepository = nuGetPackageRepository;
            NuGetPackageAuthorizationService = nuGetPackageAuthorizationService;
            BlobContainer = blobContainer;
        }

        public virtual async Task<NuGetPackageDto> GetAsync(Guid id)
        {
            await NuGetPackageAuthorizationService.CheckDefaultAsync();
            return ObjectMapper.Map<NuGetPackage, NuGetPackageDto>(await NuGetPackageRepository.GetAsync(id));
        }

        public virtual async Task<NuGetPackageDto> GetAsync(string packageName, string version)
        {
            await NuGetPackageAuthorizationService.CheckDefaultAsync();
            var package = await NuGetPackageRepository.GetAsync(GetUniqueListedSpecification(packageName, version));

            if (package == null)
                return null;

            return ObjectMapper.Map<NuGetPackage, NuGetPackageDto>(package);
        }


        public virtual async Task<byte[]> GetPackageContentAsync(string packageName, string version)
        {
            await NuGetPackageAuthorizationService.CheckDefaultAsync();
            var package = await NuGetPackageRepository.GetAsync(GetUniqueListedSpecification(packageName, version));

            if (package == null)
                return null;

            return await BlobContainer.GetAllBytesOrNullAsync(await NuGetPackageManager.GetNupkgBlobNameAsync(package));
        }

        public virtual async Task<byte[]> GetPackageManifestAsync(string packageName, string version)
        {
            await NuGetPackageAuthorizationService.CheckDefaultAsync();
            var package = await NuGetPackageRepository.GetAsync(GetUniqueListedSpecification(packageName, version));

            if (package == null)
                return null;

            return await BlobContainer.GetAllBytesOrNullAsync(await NuGetPackageManager.GetNuspecBlobNameAsync(package));
        }

        public virtual async Task<NuGetPackageDto> CreateAsync(CreateNuGetPackageInputWithStream input)
        {
            await NuGetPackageAuthorizationService.CheckCreationAsync();

            using (var packageStream = input.File.GetStream())
            {
                using (var packageReader = new PackageArchiveReader(packageStream, leaveStreamOpen: true))
                {
                    Stream readmeStream = null;
                    Stream iconStream = null;

                    var nuspecStream = await packageReader.GetNuspecAsync(default);
                    var package = await NuGetPackageManager.CreateAsync(packageReader);
                    await NuGetPackageRepository.InsertAsync(package);

                    if (package.HasReadme)
                    {
                        readmeStream = await packageReader.GetReadmeAsync();
                        await BlobContainer.SaveAsync(await NuGetPackageManager.GetReadmeBlobNameAsync(package), readmeStream);
                    }

                    if (package.HasEmbeddedIcon)
                    {
                        iconStream = await packageReader.GetIconAsync();
                        await BlobContainer.SaveAsync(await NuGetPackageManager.GetIconBlobNameAsync(package), iconStream);
                    }

                    await BlobContainer.SaveAsync(await NuGetPackageManager.GetNupkgBlobNameAsync(package), packageStream);
                    await BlobContainer.SaveAsync(await NuGetPackageManager.GetNuspecBlobNameAsync(package), nuspecStream);

                    return ObjectMapper.Map<NuGetPackage, NuGetPackageDto>(package);
                }
            }
        }

        public virtual async Task UnlistAsync(string packageName, string version)
        {
            await NuGetPackageAuthorizationService.CheckUnlistAsync();

            var package = await NuGetPackageRepository.GetAsync(
                new UniqueNuGetPackageSpecification(packageName, version),
                false);

            if (!package.Listed)
            {
                throw new BusinessException(EzGetErrorCodes.PackageAlreadyUnlisted);
            }

            package.Listed = false;
            await NuGetPackageRepository.UpdateAsync(package);
        }

        public virtual async Task RelistAsync(string packageName, string version)
        {
            await NuGetPackageAuthorizationService.CheckRelistAsync();

            var package = await NuGetPackageRepository.GetAsync(
                new UniqueNuGetPackageSpecification(packageName, version),
                false);

            if (package.Listed)
            {
                throw new BusinessException(EzGetErrorCodes.PackageAlreadyListed);
            }

            package.Listed = true;
            await NuGetPackageRepository.UpdateAsync(package);
        }

        public virtual async Task<List<string>> GetVersionListByPackageName(string packageName)
        {
            await NuGetPackageAuthorizationService.CheckDefaultAsync();
            var packages = await NuGetPackageRepository.GetListByPackageNameAsync(packageName, false);
            return packages.Select(p => p.NormalizedVersion).ToList();
        }

        private ISpecification<NuGetPackage> GetUniqueListedSpecification(string packageName, string version)
        {
            return new UniqueNuGetPackageSpecification(packageName, version)
                    .And(new ListedNuGetPackageSpecification());
        }
    }
}
