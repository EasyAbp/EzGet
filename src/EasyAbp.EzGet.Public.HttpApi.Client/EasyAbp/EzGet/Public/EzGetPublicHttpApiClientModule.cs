using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Modularity;

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
        }
    }
}
