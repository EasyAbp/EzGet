using EasyAbp.EzGet.Public.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Authorization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace EasyAbp.EzGet.Public.NuGetPackages
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(INuGetPackageAuthorizationService))]
    public class HttpApiNuGetPackageAuthorizationService : INuGetPackageAuthorizationService
    {
        protected IHttpContextAccessor HttpContextAccessor { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected ICurrentUser CurrentUser { get; }

        public HttpApiNuGetPackageAuthorizationService(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            ICurrentUser currentUser)
        {
            HttpContextAccessor = httpContextAccessor;
            AuthorizationService = authorizationService;
            CurrentUser = currentUser;
        }

        public virtual async Task CheckCreationAsync()
        {
            if (CurrentUser.IsAuthenticated)
            {
                await AuthorizationService.CheckAsync(EzGetPublicPermissions.NuGetPackages.Create);
            }
            else
            {
                var apiKey = GetNuGetApiKey();

                if (await VerifyNuGetApiKeyAsync(apiKey))
                {
                    ThrowVerifyNuGetApiKeyFailedException();
                }
            }
        }

        public virtual async Task<bool> IsGrantedCreationAsync()
        {
            if (CurrentUser.IsAuthenticated)
            {
                return await AuthorizationService.IsGrantedAsync(EzGetPublicPermissions.NuGetPackages.Create);
            }
            else
            {
                var apiKey = GetNuGetApiKey();

                if (await VerifyNuGetApiKeyAsync(apiKey))
                {
                    return false;
                }

                return true;
            }
        }

        protected virtual async Task<bool> VerifyNuGetApiKeyAsync(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return false;
            }

            //TODO: Verify the apiKey

            return true;
        }

        private string GetNuGetApiKey()
        {
            return HttpContextAccessor.HttpContext.Request.Headers[EzGetHttpApiPubilcConsts.ApiKeyHeader];
        }

        private void ThrowVerifyNuGetApiKeyFailedException()
        {
            throw new AbpAuthorizationException($"{EzGetHttpApiPubilcConsts.ApiKeyHeader} verify failed!");
        }
    }
}
