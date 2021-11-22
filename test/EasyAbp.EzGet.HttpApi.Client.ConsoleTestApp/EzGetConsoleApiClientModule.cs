using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet
{
    [DependsOn(
        typeof(EzGetCommonHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EzGetConsoleApiClientModule : AbpModule
    {
        
    }
}
