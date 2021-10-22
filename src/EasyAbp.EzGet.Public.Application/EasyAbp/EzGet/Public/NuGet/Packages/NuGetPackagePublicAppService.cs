using EasyAbp.EzGet.NuGet.Packages;
using NuGet.Packaging;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Specifications;

namespace EasyAbp.EzGet.Public.NuGet.Packages
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

        public virtual async Task<NuGetPackageDto> GetAsync(Guid id)
        {
            //TODO: Except unlist package
            return ObjectMapper.Map<NuGetPackage, NuGetPackageDto>(await NuGetPackageRepository.GetAsync(id));
        }

        public virtual async Task<NuGetPackageDto> GetAsync(string packageName, string version)
        {
            var specification = new UniqueNuGetPackageSpecification(packageName, version)
                .And(new ListedNuGetPackageSpecification());
            var package = await NuGetPackageRepository.GetAsync(specification);
            return ObjectMapper.Map<NuGetPackage, NuGetPackageDto>(package);
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
    }
}
