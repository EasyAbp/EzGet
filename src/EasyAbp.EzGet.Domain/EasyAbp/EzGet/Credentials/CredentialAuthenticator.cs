using EasyAbp.EzGet.Users;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EzGet.Credentials
{
    public class CredentialAuthenticator : ICredentialAuthenticator, ITransientDependency
    {
        protected ICredentialRepository CredentialRepository { get; }
        protected IEzGetUserLookupService EzGetUserLookupService { get; }

        public CredentialAuthenticator(
            ICredentialRepository credentialRepository,
            IEzGetUserLookupService ezGetUserLookupService)
        {
            CredentialRepository = credentialRepository;
            EzGetUserLookupService = ezGetUserLookupService;
        }

        public async Task<CredentialAuthenticateResult> AuthenticateAsync([NotNull]string credentialValue)
        {
            Check.NotNullOrWhiteSpace(credentialValue, nameof(credentialValue));

            var credential = await CredentialRepository.FindByValueAsync(credentialValue);

            if (null == credential)
            {
                return new CredentialAuthenticateResult(false, null);
            }

            var user = await EzGetUserLookupService.FindByIdAsync(credential.UserId);

            if (null == user)
            {
                return new CredentialAuthenticateResult(false, null);
            }

            return new CredentialAuthenticateResult(true, user);
        }
    }
}
