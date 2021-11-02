using EasyAbp.EzGet.Credentials;
using EasyAbp.EzGet.NuGet.Packages;
using EasyAbp.EzGet.Users;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace EasyAbp.EzGet.EntityFrameworkCore
{
    [DependsOn(
        typeof(EzGetDomainModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpUsersEntityFrameworkCoreModule)
    )]
    public class EzGetEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<EzGetDbContext>(options =>
            {
                options.AddRepository<EzGetUser, EfCoreEzGetUserRepository>();
                options.AddRepository<NuGetPackage, EfCoreNuGetPackageRepository>();
                options.AddRepository<Credential, EfCoreCredentialRepository>();
            });
        }
    }
}