using Microsoft.AspNetCore.Authentication;

namespace EasyAbp.EzGet.AspNetCore.Authentication
{
    public class EzGetCredentialAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string NuGetApiKeyHeader { get; set; }
    }
}
