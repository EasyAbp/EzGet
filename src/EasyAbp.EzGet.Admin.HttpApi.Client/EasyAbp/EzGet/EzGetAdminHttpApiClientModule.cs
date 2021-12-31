using EasyAbp.EzGet.Admin;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EzGet
{
    [DependsOn(
        typeof(EzGetCommonHttpApiClientModule),
        typeof(EzGetAdminApplicationContractsModule)
        )]
    public class EzGetAdminHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EzGetAdminApplicationContractsModule).Assembly,
                EzGetAdminRemoteServiceConsts.RemoteServiceName
            );

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EzGetAdminHttpApiClientModule>();
            });
        }
    }
}
