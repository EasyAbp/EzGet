using System;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Admin
{
    [DependsOn(
        typeof(EzGetCommonApplicationContractsModule)
        )]
    public class EzGetAdminApplicationContractsModule : AbpModule
    {
    }
}
