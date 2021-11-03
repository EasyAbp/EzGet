using EasyAbp.EzGet.Admin;
using EasyAbp.EzGet.Public;
using Volo.Abp.Modularity;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.BlobStoring;
using System.IO;

namespace EasyAbp.EzGet
{
    [DependsOn(
        typeof(EzGetAdminApplicationModule),
        typeof(EzGetPublicApplicationModule),
        typeof(AbpBlobStoringFileSystemModule),
        typeof(EzGetDomainTestModule)
        )]
    public class EzGetApplicationTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(container =>
                {
                    container.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = Path.Combine(Directory.GetCurrentDirectory(), "Pacakges");
                    });
                });
            });
        }
    }
}
