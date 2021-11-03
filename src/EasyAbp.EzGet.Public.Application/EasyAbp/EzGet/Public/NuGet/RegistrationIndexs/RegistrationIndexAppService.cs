using EasyAbp.EzGet.NuGet.Packages;
using EasyAbp.EzGet.NuGet.RegistrationIndexs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet.RegistrationIndexs
{
    public class RegistrationIndexAppService : EzGetPublicAppServiceBase, IRegistrationIndexAppService
    {
        protected INuGetPackageRepository NuGetPackageRepository { get; }
        protected IRegistrationIndexBuilder RegistrationIndexBuilder { get; }

        public RegistrationIndexAppService(
            INuGetPackageRepository nuGetPackageRepository,
            IRegistrationIndexBuilder registrationIndexBuilder)
        {
            NuGetPackageRepository = nuGetPackageRepository;
            RegistrationIndexBuilder = registrationIndexBuilder;
        }

        public virtual async Task<RegistrationIndexDto> GetAsync(string packageName)
        {
            var packageList = await NuGetPackageRepository.GetListByPackageNameAsync(packageName);
            return ObjectMapper.Map<RegistrationIndex, RegistrationIndexDto>(
                await RegistrationIndexBuilder.BuildAsync(packageList));
        }
    }
}
