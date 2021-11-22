using System;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Admin
{
    [DependsOn(
        typeof(EzGetCommonApplicationModule),
        typeof(EzGetAdminApplicationContractsModule)
        )]
    public class EzGetAdminApplicationModule : AbpModule
    {
    }
}
