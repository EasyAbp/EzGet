using Volo.Abp.Modularity;

namespace EasyAbp.EzGet
{
    [DependsOn(
        typeof(EzGetCommonApplicationModule),
        typeof(EzGetDomainTestModule)
        )]
    public class EzGetApplicationTestModule : AbpModule
    {

    }
}
