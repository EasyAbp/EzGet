using EasyAbp.EzGet.Admin;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

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
        }
    }
}
