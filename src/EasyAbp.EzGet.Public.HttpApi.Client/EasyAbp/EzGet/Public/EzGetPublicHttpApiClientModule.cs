using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EzGet.Public
{
    [DependsOn(
        typeof(EzGetCommonHttpApiClientModule),
        typeof(EzGetPublicApplicationContractsModule)
        )]
    public class EzGetPublicHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EzGetPublicApplicationContractsModule).Assembly,
                EzGetPublicRemoteServiceConsts.RemoteServiceName
            );

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EzGetPublicHttpApiClientModule>();
            });
        }
    }
}
