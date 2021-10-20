using System;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Public
{
    [DependsOn(
        typeof(EzGetCommonApplicationModule)
        )]
    public class EzGetPublicApplicationModule : AbpModule
    {
    }
}
