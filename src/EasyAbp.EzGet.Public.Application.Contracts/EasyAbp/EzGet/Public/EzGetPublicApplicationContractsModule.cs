using System;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Public
{
    [DependsOn(
        typeof(EzGetCommonApplicationContractsModule)
        )]
    public class EzGetPublicApplicationContractsModule : AbpModule
    {
    }
}
