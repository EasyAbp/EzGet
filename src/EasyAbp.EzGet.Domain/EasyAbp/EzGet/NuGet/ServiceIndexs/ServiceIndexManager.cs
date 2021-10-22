using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EzGet.NuGet.ServiceIndexs
{
    public class ServiceIndexManager : IServiceIndexManager, ITransientDependency
    {
        protected IServiceIndexUrlGenerator ServiceIndexUrlGenerator { get; }

        public ServiceIndexManager(IServiceIndexUrlGenerator serviceIndexUrlGenerator)
        {
            ServiceIndexUrlGenerator = serviceIndexUrlGenerator;
        }

        public virtual async Task<ServiceIndex> GetAsync()
        {
            return new ServiceIndex("3.0.0", await GetSupportedResourceAsync());
        }

        private async Task<List<ServiceIndexResource>> GetSupportedResourceAsync()
        {
            //TODO: Add others optional service
            var resources = new List<ServiceIndexResource>();
            resources.AddRange(
                BuildResource("PackagePublish",
                await ServiceIndexUrlGenerator.GetPackagePublishResourceUrlAsync(),
                "2.0.0"));

            resources.AddRange(
                BuildResource("SearchQueryService",
                await ServiceIndexUrlGenerator.GetSearchQueryServiceResourceUrlAsync(),
                "3.0.0-rc"));

            resources.AddRange(
                BuildResource("RegistrationsBaseUrl",
                await ServiceIndexUrlGenerator.GetRegistrationsBaseUrlResourceUrlAsync(),
                "3.0.0-rc"));

            resources.AddRange(
                BuildResource("PackageBaseAddress",
                await ServiceIndexUrlGenerator.GetPackageBaseAddressResourceUrlAsync(),
                "3.0.0"));
            return resources;
        }

        private IEnumerable<ServiceIndexResource> BuildResource(string name, string url, params string[] versions)
        {
            foreach (var version in versions)
            {
                var type = string.IsNullOrEmpty(version) ? name : $"{name}/{version}";
                yield return new ServiceIndexResource(url, type);
            }
        }
    }
}
