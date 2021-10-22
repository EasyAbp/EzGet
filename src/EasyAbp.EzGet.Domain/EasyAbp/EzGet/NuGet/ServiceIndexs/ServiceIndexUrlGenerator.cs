using EasyAbp.EzGet.Settings;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.EzGet.NuGet.ServiceIndexs
{
    public class ServiceIndexUrlGenerator : IServiceIndexUrlGenerator, ITransientDependency
    {
        protected ISettingProvider SettingProvider { get; }

        public ServiceIndexUrlGenerator(ISettingProvider settingProvider)
        {
            SettingProvider = settingProvider;
        }

        public async Task<string> GetServiceIndexUrlAsync()
        {
            return await GetHostUrlAsync() + ServiceIndexUrlConsts.ServiceIndexUrl;
        }

        public async Task<string> GetPackageBaseAddressResourceUrlAsync()
        {
            return await GetHostUrlAsync() + ServiceIndexUrlConsts.PackageBaseAddressUrl;
        }

        public async Task<string> GetRegistrationsBaseUrlResourceUrlAsync()
        {
            return await GetHostUrlAsync() + ServiceIndexUrlConsts.RegistrationsBaseUrlUrl;
        }

        public async Task<string> GetSearchQueryServiceResourceUrlAsync()
        {
            return await GetHostUrlAsync() + ServiceIndexUrlConsts.ServiceIndexUrl;
        }

        public async Task<string> GetPackagePublishResourceUrlAsync()
        {
            return await GetHostUrlAsync() + ServiceIndexUrlConsts.PackagePublishUrl;
        }

        private async Task<string> GetHostUrlAsync()
        {
            var hostUrl = await SettingProvider.GetOrNullAsync(EzGetSettingNames.HostUrl);

            if (!hostUrl.EndsWith("/"))
            {
                hostUrl = hostUrl + "/";
            }

            return hostUrl;
        }
    }
}
