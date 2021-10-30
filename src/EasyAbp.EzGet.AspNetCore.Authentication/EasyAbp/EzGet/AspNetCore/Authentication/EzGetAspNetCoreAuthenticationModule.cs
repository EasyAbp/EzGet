using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.AspNetCore.Authentication
{
    [DependsOn(
        typeof(EzGetDomainModule)
        )]
    public class EzGetAspNetCoreAuthenticationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<EzGetCredentialAuthenticationOptions>(options =>
            {
                options.NuGetApiKeyHeader = "X-NuGet-ApiKey";
            });
        }
    }
}
