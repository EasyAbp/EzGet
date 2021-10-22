using EasyAbp.EzGet.NuGet.Packages;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.EntityFrameworkCore
{
    [DependsOn(
        typeof(EzGetDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class EzGetEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<EzGetDbContext>(options =>
            {
                options.AddDefaultRepositories();
                options.AddRepository<NuGetPackage, EfCoreNuGetPackageRepository>();
            });
        }
    }
}