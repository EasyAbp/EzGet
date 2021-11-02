using System;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Admin
{
    [DependsOn(
        typeof(EzGetCommonApplicationModule)
        )]
    public class EzGetAdminApplicationModule : AbpModule
    {
    }
}
