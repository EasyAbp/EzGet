using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.NuGet.Packages;
using EasyAbp.EzGet.NuGet.RegistrationIndexs;
using EasyAbp.EzGet.Public.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet.RegistrationIndexs
{
    public class RegistrationIndexPublicAppService : EzGetPublicNuGetAppServiceBase, IRegistrationIndexPublicAppService
    {
        protected INuGetPackageRepository NuGetPackageRepository { get; }
        protected IRegistrationIndexBuilder RegistrationIndexBuilder { get; }

        public RegistrationIndexPublicAppService(
            INuGetPackageRepository nuGetPackageRepository,
            IRegistrationIndexBuilder registrationIndexBuilder,
            IFeedStore feedStore) : base(feedStore)
        {
            NuGetPackageRepository = nuGetPackageRepository;
            RegistrationIndexBuilder = registrationIndexBuilder;
        }

        [AllowAnonymousIfFeedPublic]
        [Authorize(EzGetPublicPermissions.RegistrationIndexs.Default)]
        public virtual async Task<RegistrationIndexDto> GetIndexAsync(string packageName, string feedName)
        {
            var packageList = await NuGetPackageRepository.GetListByPackageNameAndFeedIdAsync(
                packageName,
                null,
                null,
                await GetFeedIdAsync(feedName));

            return ObjectMapper.Map<RegistrationIndex, RegistrationIndexDto>(
                await RegistrationIndexBuilder.BuildIndexAsync(packageList, feedName));
        }

        [AllowAnonymousIfFeedPublic]
        [Authorize(EzGetPublicPermissions.RegistrationIndexs.Default)]
        public virtual async Task<RegistrationLeafDto> GetLeafAsync(string pacakgeName, string version, string feedName)
        {
            var package = await NuGetPackageRepository.GetAsync(pacakgeName, version, await GetFeedIdAsync(feedName), null);

            return ObjectMapper.Map<RegistrationLeaf, RegistrationLeafDto>(
                await RegistrationIndexBuilder.BuildLeafAsync(package, feedName));
        }
    }
}
