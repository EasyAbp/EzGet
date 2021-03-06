using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.AspNetCore.Authentication
{
    public class EzGetBasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        protected IEzGetUserAuthenticator EzGetUserAuthenticator { get; }
        protected EzGetUserClaimsPrincipalGenerator EzGetUserClaimsPrincipalGenerator { get; }
        protected IOptions<FeedOptions> FeedOptions { get; }
        protected IFeedStore FeedStore { get; }

        private const string _authorizationBasicHeader = "Authorization";
        private const string _authorizationType = "Basic";

        public EzGetBasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IEzGetUserAuthenticator ezGetUserAuthenticator,
            EzGetUserClaimsPrincipalGenerator ezGetUserClaimsPrincipalGenerator,
            IOptions<FeedOptions> feedOptions,
            IFeedStore feedStore)
            : base(options, logger, encoder, clock)
        {
            EzGetUserAuthenticator = ezGetUserAuthenticator;
            EzGetUserClaimsPrincipalGenerator = ezGetUserClaimsPrincipalGenerator;
            FeedOptions = feedOptions;
            FeedStore = feedStore;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.RouteValues.TryGetValue(FeedOptions.Value.FeedRouteName, out var feedName) && null != feedName)
            {
                var feed = await FeedStore.GetAsync(feedName.ToString());
                if (feed.FeedType == FeedTypeEnum.Public)
                {
                    return AuthenticateResult.NoResult();
                }
            }

            if (!Request.Headers.ContainsKey(_authorizationBasicHeader))
            {
                return Fail("Protocol error!");
            }

            var headerValue = Request.Headers[_authorizationBasicHeader].ToString();

            if (string.IsNullOrWhiteSpace(headerValue) || !headerValue.StartsWith(_authorizationType + " "))
            {
                return Fail("Token is empty or white space or not Basic Authentication");
            }

            var token = headerValue[headerValue.IndexOf(" ")..];
            var decodeToken = DecodeBase64(token);
            var userName = decodeToken[..decodeToken.IndexOf(":")];
            var password = decodeToken[(decodeToken.IndexOf(":") + 1)..];

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return Fail("UserName or Passowrd is empty or white space");
            }

            var result = await EzGetUserAuthenticator.AuthenticateAsync(userName, password);

            if (result.Success)
            {
                var principal = await EzGetUserClaimsPrincipalGenerator.GenerateAsync(result.User);
                return AuthenticateResult.Success(new AuthenticationTicket(
                    principal,
                    EzGetAspNetCoreAuthenticationConsts.EzGetBasicAuthenticationScheme));
            }

            return Fail("UserName or Passsword error");
        }

        private AuthenticateResult Fail(string failureMessage)
        {
            Response.Headers.Add("WWW-Authenticate", "Basic realm=\"EzGet\"");
            return AuthenticateResult.Fail(failureMessage);
        }

        private string DecodeBase64(string code)
        {
            string decode;
            byte[] bytes = Convert.FromBase64String(code);

            try
            {
                decode = Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                decode = code;
            }

            return decode;
        }
    }
}
