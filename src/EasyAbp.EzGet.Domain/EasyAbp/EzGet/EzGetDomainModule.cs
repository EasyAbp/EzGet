using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Users;
using Volo.Abp.BlobStoring;
using System.IO;

namespace EasyAbp.EzGet
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpUsersDomainModule),
        typeof(AbpBlobStoringModule),
        typeof(EzGetDomainSharedModule)
    )]
    public class EzGetDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PacakgeBlobNameOptions>(options =>
            {
                options.BlobNameSeparator = Path.PathSeparator.ToString();
            });
        }
    }
}
