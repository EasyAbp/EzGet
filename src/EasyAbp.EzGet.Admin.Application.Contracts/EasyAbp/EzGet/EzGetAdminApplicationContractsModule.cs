using System;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Admin.Application.Contracts
{
    [DependsOn(
        typeof(EzGetCommonApplicationContractsModule)
        )]
    public class EzGetAdminApplicationContractsModule : AbpModule
    {
    }
}
