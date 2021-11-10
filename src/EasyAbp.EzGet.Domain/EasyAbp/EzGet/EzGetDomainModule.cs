using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Users;
using Volo.Abp.BlobStoring;
using System.IO;
using Volo.Abp.Caching;

namespace EasyAbp.EzGet
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpUsersDomainModule),
        typeof(AbpBlobStoringModule),
        typeof(AbpCachingModule),
        typeof(EzGetDomainSharedModule)
    )]
    public class EzGetDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PacakgeBlobNameOptions>(options =>
            {
                options.BlobNameSeparator = Path.DirectorySeparatorChar.ToString();
            });
        }
    }
}
