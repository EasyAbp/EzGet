using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Identity;

namespace EasyAbp.EzGet.Users
{
    public class AbpIdentityDomainUserAuthenticator : IEzGetUserAuthenticator
    {
        protected IIdentityUserRepository IdentityUserRepository { get; }
        protected UserManager<IdentityUser> UserManager { get; }
        protected IEzGetUserLookupService EzGetUserLookupService { get; }

        public AbpIdentityDomainUserAuthenticator(
            IIdentityUserRepository identityUserRepository,
            UserManager<IdentityUser> userManager,
            IEzGetUserLookupService ezGetUserLookupService)
        {
            IdentityUserRepository = identityUserRepository;
            UserManager = userManager;
            EzGetUserLookupService = ezGetUserLookupService;
        }

        public async Task<EzGetUserAuthenticateionResult> AuthenticateAsync([NotNull] string userName, [NotNull] string password)
        {
            Check.NotNullOrWhiteSpace(userName, nameof(userName));
            Check.NotNullOrWhiteSpace(password, nameof(password));

            var identityUser = await IdentityUserRepository.FindByNormalizedUserNameAsync(userName);

            if (null == identityUser)
            {
                return new EzGetUserAuthenticateionResult(false, null);
            }

            var success = await UserManager.CheckPasswordAsync(identityUser, password);

            if (success)
            {
                var user = await EzGetUserLookupService.FindByIdAsync(identityUser.Id);
                return new EzGetUserAuthenticateionResult(success, user);
            }

            return new EzGetUserAuthenticateionResult(false, null);
        }
    }
}
