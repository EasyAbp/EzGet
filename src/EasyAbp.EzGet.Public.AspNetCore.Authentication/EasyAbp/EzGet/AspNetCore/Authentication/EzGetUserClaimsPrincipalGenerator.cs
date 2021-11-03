using EasyAbp.EzGet.Users;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;

namespace EasyAbp.EzGet.AspNetCore.Authentication
{
    public class EzGetUserClaimsPrincipalGenerator : ITransientDependency
    {
        protected IUserRoleLookupService UserRoleLookupService { get; }

        public EzGetUserClaimsPrincipalGenerator(IUserRoleLookupService userRoleLookupService)
        {
            UserRoleLookupService = userRoleLookupService;
        }

        public virtual async Task<ClaimsPrincipal> GenerateAsync(EzGetUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(AbpClaimTypes.UserId, user.Id.ToString()),
                new Claim(AbpClaimTypes.UserName, user.UserName ?? string.Empty),
                new Claim(AbpClaimTypes.Name, user.Name ?? string.Empty),
                new Claim(AbpClaimTypes.SurName, user.Surname ?? string.Empty),
                new Claim(AbpClaimTypes.PhoneNumber, user.PhoneNumber ?? string.Empty),
                new Claim(AbpClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed.ToString()),
                new Claim(AbpClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(AbpClaimTypes.EmailVerified, user.EmailConfirmed.ToString()),
            };

            var userRoles = await UserRoleLookupService.FindRolesAsync(user.Id);
            claims.AddRange(userRoles.Select(p => new Claim(AbpClaimTypes.Role, p)));

            var claimIdentity = new ClaimsIdentity(claims, EzGetAspNetCoreAuthenticationConsts.EzGetCredentialAuthenticationScheme);

            return new ClaimsPrincipal(claimIdentity);
        }
    }
}
