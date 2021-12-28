using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.Settings;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.EzGet.NuGet.ServiceIndexs
{
    public class ServiceIndexUrlGenerator : IServiceIndexUrlGenerator, ITransientDependency
    {
        protected IEzGetConfiguration EzGetConfiguration { get; }
        protected IOptions<FeedOptions> FeedOptions { get; }

        public ServiceIndexUrlGenerator(
            IEzGetConfiguration ezGetConfiguration,
            IOptions<FeedOptions> feedOptions)
        {
            EzGetConfiguration = ezGetConfiguration;
            FeedOptions = feedOptions;
        }

        public virtual async Task<string> GetServiceIndexUrlAsync(string feedName)
        {
            return await GetHostUrlWithFeedAsync(feedName) + ServiceIndexUrlConsts.ServiceIndexUrl;
        }

        public virtual async Task<string> GetPackageBaseAddressResourceUrlAsync(string feedName)
        {
            return await GetHostUrlWithFeedAsync(feedName) + ServiceIndexUrlConsts.PackageBaseAddressUrl;
        }

        public virtual async Task<string> GetRegistrationsBaseUrlResourceUrlAsync(string feedName)
        {
            return await GetHostUrlWithFeedAsync(feedName) + ServiceIndexUrlConsts.RegistrationsBaseUrlUrl;
        }

        public virtual async Task<string> GetRegistrationIndexUrlAsync(string id, string feedName)
        {
            return $"{await GetHostUrlWithFeedAsync(feedName)}{ServiceIndexUrlConsts.RegistrationsBaseUrlUrl}/{id}/index.json";
        }

        public virtual async Task<string> GetAutoCompleteUrlAsync(string feedName)
        {
            return await GetHostUrlWithFeedAsync(feedName) + ServiceIndexUrlConsts.AutocompleteUrl;
        }

        public virtual async Task<string> GetRegistrationLeafUrlAsync(string id, string version, string feedName)
        {
            return $"{await GetHostUrlWithFeedAsync(feedName)}{ServiceIndexUrlConsts.RegistrationsBaseUrlUrl}/{id}/{version}.json";
        }

        public virtual async Task<string> GetSearchQueryServiceResourceUrlAsync(string feedName)
        {
            return await GetHostUrlWithFeedAsync(feedName) + ServiceIndexUrlConsts.SearchQueryServiceUrl;
        }

        public virtual async Task<string> GetPackagePublishResourceUrlAsync(string feedName)
        {
            return await GetHostUrlWithFeedAsync(feedName) + ServiceIndexUrlConsts.PackagePublishUrl;
        }

        public virtual async Task<string> GetPackageDownloadUrlAsync(string id, string version, string feedName)
        {
            return $"{await GetHostUrlWithFeedAsync(feedName)}{ServiceIndexUrlConsts.PackageBaseAddressUrl}/{id}/{version}/{id}.{version}.nupkg";
        }

        public virtual async Task<string> GetPacakgeIconUrlAsync(string id, string version, string feedName)
        {
            return $"{await GetHostUrlWithFeedAsync(feedName)}{ServiceIndexUrlConsts.PackageBaseAddressUrl}/{id}/{version}/icon";
        }

        public virtual async Task<string> GetPackageReadmeUrlAsync(string id, string version, string feedName)
        {
            return $"{await GetHostUrlWithFeedAsync(feedName)}{ServiceIndexUrlConsts.PackageBaseAddressUrl}/{id}/{version}/readme";
        }

        private async Task<string> GetHostUrlWithFeedAsync(string feedName)
        {
            var hostUrl = await EzGetConfiguration.GetHostUrlAsync();
            var feed = GetFeed(feedName);

            if (string.IsNullOrEmpty(feed))
            {
                return hostUrl;
            }

            if (!feed.EndsWith("/"))
            {
                feed += "/";
            }

            return $"{hostUrl}{feed}";
        }

        private string GetFeed(string feedName)
        {
            return string.IsNullOrEmpty(feedName) ? null : string.Format(FeedOptions.Value.FeedPatternFormat, feedName);
        }
    }
}
