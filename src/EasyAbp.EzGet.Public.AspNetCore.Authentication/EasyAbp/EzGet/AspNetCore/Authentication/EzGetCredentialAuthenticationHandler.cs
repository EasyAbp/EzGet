using EasyAbp.EzGet.Credentials;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.AspNetCore.Authentication
{
    public class EzGetCredentialAuthenticationHandler : AuthenticationHandler<EzGetCredentialAuthenticationOptions>
    {
        protected ICredentialAuthenticator CredentialAuthenticator { get; }

        public EzGetCredentialAuthenticationHandler(
            IOptionsMonitor<EzGetCredentialAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ICredentialAuthenticator credentialAuthenticator)
            : base(options, logger, encoder, clock)
        {
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
                return AuthenticateResult.Fail("Can not find ApiKey in http headers");
            }

            //TODO: Get Credential, find user, create ClaimsPrincipal
            var result = await CredentialAuthenticator.AuthenticateAsync(apiKey);

            if (result.Success)
            {
                var principal = new ClaimsPrincipal();
                return AuthenticateResult.Success(new AuthenticationTicket(
                    principal,
                    EzGetAspNetCoreAuthenticationConsts.EzGetCredentialAuthenticationScheme));
            }

            return AuthenticateResult.NoResult();
        }
    }
}
