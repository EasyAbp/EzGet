using System;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Public.Application.Contracts
{
    [DependsOn(
        typeof(EzGetCommonApplicationContractsModule)
        )]
    public class EzGetPublicApplicationContractsModule : AbpModule
    {
    }
}
