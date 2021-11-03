using EasyAbp.EzGet.Credentials;
using EasyAbp.EzGet.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Volo.Abp.Security.Claims;

namespace EasyAbp.EzGet.AspNetCore.Authentication
{
    public class EzGetCredentialAuthenticationHandler : AuthenticationHandler<EzGetCredentialAuthenticationOptions>
    {
        protected ICredentialAuthenticator CredentialAuthenticator { get; }
        protected IUserRoleLookupService UserRoleLookupService { get; }
        protected EzGetUserClaimsPrincipalGenerator EzGetUserClaimsPrincipalGenerator { get; }

        public EzGetCredentialAuthenticationHandler(
            IOptionsMonitor<EzGetCredentialAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ICredentialAuthenticator credentialAuthenticator,
            EzGetUserClaimsPrincipalGenerator ezGetUserClaimsPrincipalGenerator)
            : base(options, logger, encoder, clock)
        {
            CredentialAuthenticator = credentialAuthenticator;
            EzGetUserClaimsPrincipalGenerator = ezGetUserClaimsPrincipalGenerator;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(Options.NuGetApiKeyHeader))
            {
                return AuthenticateResult.NoResult();
            }

            var apiKey = Request.Headers[Options.NuGetApiKeyHeader];

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return AuthenticateResult.Fail("ApiKey is empty or white space");
            }

            var result = await CredentialAuthenticator.AuthenticateAsync(apiKey);

            if (result.Success)
            {
                var principal = await EzGetUserClaimsPrincipalGenerator.GenerateAsync(result.User);
                return AuthenticateResult.Success(new AuthenticationTicket(
                    principal,
                    EzGetAspNetCoreAuthenticationConsts.EzGetCredentialAuthenticationScheme));
            }

            return AuthenticateResult.Fail("ApiKey authenticate fail");
        }
    }
}
