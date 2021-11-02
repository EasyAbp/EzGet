using EasyAbp.EzGet.Credentials;
using EasyAbp.EzGet.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Volo.Abp.Security.Claims;

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
            CredentialAuthenticator = credentialAuthenticator;
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
                var principal = CreateClaimsPrincipal(result.User);
                return AuthenticateResult.Success(new AuthenticationTicket(
                    principal,
                    EzGetAspNetCoreAuthenticationConsts.EzGetCredentialAuthenticationScheme));
            }

            return AuthenticateResult.Fail("ApiKey authenticate fail");
        }

        private ClaimsPrincipal CreateClaimsPrincipal(EzGetUser user)
        {
            var claims = new Claim[]
            {
                new Claim(AbpClaimTypes.UserId, user.Id.ToString()),
                new Claim(AbpClaimTypes.UserName, user.UserName ?? string.Empty),
                new Claim(AbpClaimTypes.Name, user.Name ?? string.Empty),
                new Claim(AbpClaimTypes.SurName, user.Surname ?? string.Empty),
                new Claim(AbpClaimTypes.PhoneNumber, user.PhoneNumber ?? string.Empty),
                new Claim(AbpClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed.ToString()),
                new Claim(AbpClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(AbpClaimTypes.EmailVerified, user.EmailConfirmed.ToString()),
                //TODO: How to get roles through remote service?
                new Claim(AbpClaimTypes.Role, "")
            };

            var claimIdentity = new ClaimsIdentity(claims, EzGetAspNetCoreAuthenticationConsts.EzGetCredentialAuthenticationScheme);

            return new ClaimsPrincipal(claimIdentity);
        }
    }
}
