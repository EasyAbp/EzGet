using EasyAbp.EzGet.Settings;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.EzGet.NuGet.ServiceIndexs
{
    public class ServiceIndexUrlGenerator : IServiceIndexUrlGenerator, ITransientDependency
    {
        protected IEzGetConfiguration EzGetConfiguration { get; }

        public ServiceIndexUrlGenerator(IEzGetConfiguration ezGetConfiguration)
        {
            EzGetConfiguration = ezGetConfiguration;
        }

        public async Task<string> GetServiceIndexUrlAsync()
        {
            return await EzGetConfiguration.GetHostUrlAsync() + ServiceIndexUrlConsts.ServiceIndexUrl;
        }

        public async Task<string> GetPackageBaseAddressResourceUrlAsync()
        {
            return await EzGetConfiguration.GetHostUrlAsync() + ServiceIndexUrlConsts.PackageBaseAddressUrl;
        }

        public async Task<string> GetRegistrationsBaseUrlResourceUrlAsync()
        {
            return await EzGetConfiguration.GetHostUrlAsync() + ServiceIndexUrlConsts.RegistrationsBaseUrlUrl;
        }

        public async Task<string> GetSearchQueryServiceResourceUrlAsync()
        {
            return await EzGetConfiguration.GetHostUrlAsync() + ServiceIndexUrlConsts.ServiceIndexUrl;
        }

        public async Task<string> GetPackagePublishResourceUrlAsync()
        {
            return await EzGetConfiguration.GetHostUrlAsync() + ServiceIndexUrlConsts.PackagePublishUrl;
        }
    }
}
