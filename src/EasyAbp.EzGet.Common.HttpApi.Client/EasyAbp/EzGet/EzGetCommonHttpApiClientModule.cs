using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet
{
    [DependsOn(
        typeof(EzGetCommonApplicationContractsModule),
        typeof(AbpHttpClientModule)
        )]
    public class EzGetCommonHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EzGetCommonApplicationContractsModule).Assembly,
                EzGetCommonRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}
