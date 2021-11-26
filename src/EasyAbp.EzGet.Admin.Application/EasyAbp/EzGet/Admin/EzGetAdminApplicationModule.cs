using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Admin
{
    [DependsOn(
        typeof(EzGetCommonApplicationModule),
        typeof(EzGetAdminApplicationContractsModule)
        )]
    public class EzGetAdminApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<EzGetAdminApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EzGetAdminApplicationModule>(validate: true);
            });
        }
    }
}
