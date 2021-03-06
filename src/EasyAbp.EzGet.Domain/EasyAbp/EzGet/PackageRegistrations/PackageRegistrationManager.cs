using EasyAbp.EzGet.Users;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace EasyAbp.EzGet.PackageRegistrations
{
    public class PackageRegistrationManager : DomainService, IPackageRegistrationManager
    {
        protected IPackageRegistrationRepository PackageRegistrationRepository { get; }
        protected ICurrentUser CurrentUser { get; }

        public PackageRegistrationManager(
            IPackageRegistrationRepository packageRegistrationRepository,
            ICurrentUser currentUser)
        {
            PackageRegistrationRepository = packageRegistrationRepository;
            CurrentUser = currentUser;
        }

        public virtual async Task<PackageRegistration> CreateOrUpdateAsync(
            [NotNull] string packageName,
            [NotNull] string type,
            Guid? feedId,
            [NotNull] string version,
            long size,
            string description)
        {
            Check.NotNullOrWhiteSpace(packageName, nameof(packageName));
            Check.NotNullOrWhiteSpace(type, nameof(type));
            Check.NotNullOrWhiteSpace(version, nameof(version));

            var packageRegistration = await PackageRegistrationRepository.FindByNameAndTypeAsync(
                packageName,
                type,
                feedId);

            if (null == packageRegistration)
            {
                packageRegistration = new PackageRegistration(
                        GuidGenerator.Create(),
                        feedId,
                        packageName,
                        type,
                        version,
                        size,
                        description);

                if (CurrentUser.Id.HasValue)
                {
                    packageRegistration.AddOwnerId(CurrentUser.Id.Value, PackageRegistrationOwnerTypeEnum.Maintainer);
                }

                await PackageRegistrationRepository.InsertAsync(packageRegistration);
            }
            else
            {
                packageRegistration.SetLastVersion(version);
                packageRegistration.Size = size;

                await PackageRegistrationRepository.UpdateAsync(packageRegistration);
            }

            return packageRegistration;
        }
    }
}
