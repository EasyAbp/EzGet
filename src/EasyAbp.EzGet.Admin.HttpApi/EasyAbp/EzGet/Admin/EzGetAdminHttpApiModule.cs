using System;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace EasyAbp.EzGet.Admin
{
    [DependsOn(
        typeof(EzGetCommonHttpApiModule),
        typeof(EzGetAdminApplicationContractsModule)
        )]
    public class EzGetAdminHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //Configure<AbpAspNetCoreMvcOptions>(options =>
            //{
            //    options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(CreateNuGetPackageInputWithStream));
            //});
        }
    }
}
