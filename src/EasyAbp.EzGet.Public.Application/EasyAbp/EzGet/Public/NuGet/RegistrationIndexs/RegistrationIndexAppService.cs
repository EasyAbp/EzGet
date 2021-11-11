using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.NuGet.Packages;
using EasyAbp.EzGet.NuGet.RegistrationIndexs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet.RegistrationIndexs
{
    public class RegistrationIndexAppService : EzGetPublicNuGetAppServiceBase, IRegistrationIndexAppService
    {
        protected INuGetPackageRepository NuGetPackageRepository { get; }
        protected IRegistrationIndexBuilder RegistrationIndexBuilder { get; }

        public RegistrationIndexAppService(
            INuGetPackageRepository nuGetPackageRepository,
            IRegistrationIndexBuilder registrationIndexBuilder,
            IFeedStore feedStore) : base(feedStore)
        {
            NuGetPackageRepository = nuGetPackageRepository;
            RegistrationIndexBuilder = registrationIndexBuilder;
        }

        public virtual async Task<RegistrationIndexDto> GetIndexAsync(string packageName, string feedName)
        {
            var packageList = await NuGetPackageRepository.GetListByPackageNameAndFeedIdAsync(
                packageName,
                await GetFeedIdAsync(feedName));

            return ObjectMapper.Map<RegistrationIndex, RegistrationIndexDto>(
                await RegistrationIndexBuilder.BuildIndexAsync(packageList, feedName));
        }

        public virtual async Task<RegistrationLeafDto> GetLeafAsync(string pacakgeName, string version, string feedName)
        {
            var package = await NuGetPackageRepository.GetAsync(
                new UniqueNuGetPackageSpecification(pacakgeName, version, await GetFeedIdAsync(feedName)));

            return ObjectMapper.Map<RegistrationLeaf, RegistrationLeafDto>(
                await RegistrationIndexBuilder.BuildLeafAsync(package, feedName));
        }
    }
}
