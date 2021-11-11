using EasyAbp.EzGet.Feeds;
using Microsoft.Extensions.Options;
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
        protected IOptions<FeedOptions> FeedOptions { get; }

        public ServiceIndexManager(
            IServiceIndexUrlGenerator serviceIndexUrlGenerator,
            IOptions<FeedOptions> feedOptions)
        {
            ServiceIndexUrlGenerator = serviceIndexUrlGenerator;
            FeedOptions = feedOptions;
        }

        public virtual async Task<ServiceIndex> GetAsync(string feedName)
        {
            return new ServiceIndex("3.0.0", await GetSupportedResourceAsync(feedName));
        }

        private async Task<List<ServiceIndexResource>> GetSupportedResourceAsync(string feedName)
        {
            //TODO: Add others optional service
            var resources = new List<ServiceIndexResource>();
            resources.AddRange(
                BuildResource("PackagePublish",
                await ServiceIndexUrlGenerator.GetPackagePublishResourceUrlAsync(feedName),
                "2.0.0"));

            resources.AddRange(
                BuildResource("SearchQueryService",
                await ServiceIndexUrlGenerator.GetSearchQueryServiceResourceUrlAsync(feedName),
                "3.0.0-rc", "3.0.0-beta", string.Empty));

            resources.AddRange(
                BuildResource("RegistrationsBaseUrl",
                await ServiceIndexUrlGenerator.GetRegistrationsBaseUrlResourceUrlAsync(feedName),
                "3.0.0-rc"));

            resources.AddRange(
                BuildResource("PackageBaseAddress",
                await ServiceIndexUrlGenerator.GetPackageBaseAddressResourceUrlAsync(feedName),
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
