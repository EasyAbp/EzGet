using System;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Public.Application
{
    [DependsOn(
        typeof(EzGetCommonApplicationModule)
        )]
    public class EzGetPublicApplicationModule : AbpModule
    {
    }
}
