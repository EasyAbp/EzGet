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

        public virtual async Task<string> GetServiceIndexUrlAsync()
        {
            return await EzGetConfiguration.GetHostUrlAsync() + ServiceIndexUrlConsts.ServiceIndexUrl;
        }

        public virtual async Task<string> GetPackageBaseAddressResourceUrlAsync()
        {
            return await EzGetConfiguration.GetHostUrlAsync() + ServiceIndexUrlConsts.PackageBaseAddressUrl;
        }

        public virtual async Task<string> GetRegistrationsBaseUrlResourceUrlAsync()
        {
            return await EzGetConfiguration.GetHostUrlAsync() + ServiceIndexUrlConsts.RegistrationsBaseUrlUrl;
        }

        public virtual async Task<string> GetRegistrationIndexUrlAsync(string id)
        {
            return $"{await EzGetConfiguration.GetHostUrlAsync()}{ServiceIndexUrlConsts.RegistrationsBaseUrlUrl}/{id}/index.json";
        }

        public virtual async Task<string> GetRegistrationLeafUrlAsync(string id, string version)
        {
            return $"{await EzGetConfiguration.GetHostUrlAsync()}{ServiceIndexUrlConsts.RegistrationsBaseUrlUrl}/{id}/{version}.json";
        }

        public virtual async Task<string> GetSearchQueryServiceResourceUrlAsync()
        {
            return await EzGetConfiguration.GetHostUrlAsync() + ServiceIndexUrlConsts.ServiceIndexUrl;
        }

        public virtual async Task<string> GetPackagePublishResourceUrlAsync()
        {
            return await EzGetConfiguration.GetHostUrlAsync() + ServiceIndexUrlConsts.PackagePublishUrl;
        }

        public virtual async Task<string> GetPackageDownloadUrlAsync(string id, string version)
        {
            return $"{await EzGetConfiguration.GetHostUrlAsync()}{ServiceIndexUrlConsts.PackageBaseAddressUrl}/{id}/{version}/{id}.{version}.nupkg";
        }

        public virtual async Task<string> GetPacakgeIconUrlAsync(string id, string version)
        {
            return $"{await EzGetConfiguration.GetHostUrlAsync()}{ServiceIndexUrlConsts.PackageBaseAddressUrl}/{id}/{version}/icon";
        }

        public virtual async Task<string> GetPackageReadmeUrlAsync(string id, string version)
        {
            return $"{await EzGetConfiguration.GetHostUrlAsync()}{ServiceIndexUrlConsts.PackageBaseAddressUrl}/{id}/{version}/readme";
        }
    }
}
