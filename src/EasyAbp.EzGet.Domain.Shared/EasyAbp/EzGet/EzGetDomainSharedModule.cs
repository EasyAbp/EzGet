using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.EzGet.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Users;

namespace EasyAbp.EzGet
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(AbpUsersDomainSharedModule)
    )]
    public class EzGetDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EzGetDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<EzGetResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/EasyAbp/EzGet/Localization/EzGet");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EzGet", typeof(EzGetResource));
            });
        }
    }
}
