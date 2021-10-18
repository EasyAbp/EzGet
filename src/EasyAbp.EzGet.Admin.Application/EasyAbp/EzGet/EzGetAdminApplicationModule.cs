using System;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Admin.Application
{
    [DependsOn(
        typeof(EzGetCommonApplicationModule)
        )]
    public class EzGetAdminApplicationModule : AbpModule
    {
    }
}
