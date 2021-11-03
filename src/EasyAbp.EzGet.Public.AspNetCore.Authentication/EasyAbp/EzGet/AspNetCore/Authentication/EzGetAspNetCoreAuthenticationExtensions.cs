using Microsoft.AspNetCore.Authentication;
using System;

namespace EasyAbp.EzGet.AspNetCore.Authentication
{
    public static class EzGetAspNetCoreAuthenticationExtensions
    {
        public static AuthenticationBuilder AddEzGetCredential(
            this AuthenticationBuilder builder)
        {
            return builder.AddScheme<EzGetCredentialAuthenticationOptions, EzGetCredentialAuthenticationHandler>(
                EzGetAspNetCoreAuthenticationConsts.EzGetCredentialAuthenticationScheme,
                null,
                options => options.NuGetApiKeyHeader = EzGetAspNetCoreAuthenticationConsts.NuGetApiKeyHeader);
        }

        public static AuthenticationBuilder AddEzGetCredential(
            this AuthenticationBuilder builder,
            Action<EzGetCredentialAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<EzGetCredentialAuthenticationOptions, EzGetCredentialAuthenticationHandler>(
                EzGetAspNetCoreAuthenticationConsts.EzGetCredentialAuthenticationScheme,
                null,
                configureOptions);
        }

        public static AuthenticationBuilder AddEzGetCredential(
            this AuthenticationBuilder builder,
            string authenticationScheme,
            Action<EzGetCredentialAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<EzGetCredentialAuthenticationOptions, EzGetCredentialAuthenticationHandler>(
                authenticationScheme,
                null,
                configureOptions);
        }

        public static AuthenticationBuilder AddEzGetCredential(
            this AuthenticationBuilder builder,
            string authenticationScheme,
            string displayName,
            Action<EzGetCredentialAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<EzGetCredentialAuthenticationOptions, EzGetCredentialAuthenticationHandler>(
                authenticationScheme,
                displayName,
                configureOptions);
        }
    }
}
