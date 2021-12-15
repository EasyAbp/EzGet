using EasyAbp.EzGet.EntityFrameworkCore;
using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EzGet.NuGet.Packages
{
    //TODO: Implementation using ES
    public class EfCoreNuGetPackageSearcher :
        EfCoreRepository<IEzGetDbContext, NuGetPackage, Guid>,
        INuGetPackageSearcher,
        ITransientDependency
    {
        protected IServiceIndexUrlGenerator ServiceIndexUrlGenerator { get; }
        protected IFeedStore FeedStore { get; }

        public EfCoreNuGetPackageSearcher(
            IDbContextProvider<IEzGetDbContext> dbContextProvider,
            IServiceIndexUrlGenerator serviceIndexUrlGenerator,
            IFeedStore feedStore)
            : base(dbContextProvider)
        {
            ServiceIndexUrlGenerator = serviceIndexUrlGenerator;
            FeedStore = feedStore;
        }

        public virtual async Task<NuGetPackageSearchListResult> SearchListAsync(
            int skip,
            int take,
            string filter,
            bool includePrerelease,
            bool includeSemVer2,
            string packageType = null,
            string feedName = null,
            CancellationToken cancellationToken = default)
        {
            Guid? feedId = null;

            if (!string.IsNullOrEmpty(feedName))
            {
                feedId = (await FeedStore.GetAsync(feedName)).Id;
            }

            var count = await SearchListCountImplAsync(
                filter,
                includePrerelease,
                includeSemVer2,
                packageType,
                feedId,
                cancellationToken);

            var packageResultList = new List<NuGetPackageSearchResult>();
            var packages = await SearchListImplAsync(
                skip,
                take,
                filter,
                includePrerelease,
                includeSemVer2,
                packageType,
                feedId,
                cancellationToken);

            foreach (var package in packages)
            {
                var versions = package.OrderByDescending(p => p.GetNuGetVersion()).ToList();
                var latest = versions.First();
                var iconUrl = latest.HasEmbeddedIcon ?
                    await ServiceIndexUrlGenerator.GetPacakgeIconUrlAsync(latest.PackageName, latest.GetNuGetVersion().ToNormalizedString().ToLowerInvariant(), feedName) :
                    latest.IconUrl.GetAbsoluteUriOrEmpty();

                var packageResult = new NuGetPackageSearchResult(
                    latest.PackageName,
                    latest.GetNuGetVersion().ToFullString(),
                    latest.Description,
                    latest.Authors,
                    iconUrl,
                    latest.LicenseUrl.GetAbsoluteUriOrEmpty(),
                    latest.ProjectUrl.GetAbsoluteUriOrEmpty(),
                    await ServiceIndexUrlGenerator.GetRegistrationIndexUrlAsync(latest.PackageName, feedName),
                    latest.Summary,
                    latest.Tags,
                    latest.Downloads,
                    latest.Title,
                    GetSearchResultPackageTypes(versions),
                    await GetSearchResultVersionsAsync(versions, feedName));

                packageResultList.Add(packageResult);
            }

            return new NuGetPackageSearchListResult(count, packageResultList);
        }

        private async Task<List<SearchResultVersion>> GetSearchResultVersionsAsync(List<NuGetPackage> packages, string feedName)
        {
            var result = new List<SearchResultVersion>();

            foreach (var package in packages)
            {
                result.Add(new SearchResultVersion(
                    await ServiceIndexUrlGenerator.GetRegistrationLeafUrlAsync(package.PackageName, package.GetNuGetVersion().ToNormalizedString().ToLowerInvariant(), feedName),
                    package.GetNuGetVersion().ToFullString(),
                    package.Downloads));
            }

            return result;
        }

        private List<SearchResultPackageType> GetSearchResultPackageTypes(List<NuGetPackage> packages)
        {
            var result = new List<SearchResultPackageType>();

            foreach (var package in packages)
            {
                foreach (var packageType in package.PackageTypes)
                {
                    if (!result.Any(p => p.Name == packageType.Name))
                    {
                        result.Add(new SearchResultPackageType(packageType.Name));
                    }
                }
            }

            return result;
        }

        private async Task<List<IGrouping<string, NuGetPackage>>> SearchListImplAsync(
            int skip,
            int take,
            string filter,
            bool includePrerelease,
            bool includeSemVer2,
            string packageType,
            Guid? feedId,
            CancellationToken cancellationToken = default)
        {
            filter = filter?.ToLower();

            var query = AddSearchListFilters(await GetQueryableAsync(), filter, includePrerelease, includeSemVer2, packageType, feedId)
                .Select(p => p.PackageName)
                .OrderBy(p => p)
                .Skip(skip)
                .Take(take);

            var packageNames = await query.ToListAsync(GetCancellationToken(cancellationToken));

            var packages = await AddSearchListFilters(await WithDetailsAsync(), filter, includePrerelease, includeSemVer2, packageType, feedId)
                .Where(p => packageNames.Contains(p.PackageName))
                .ToListAsync(GetCancellationToken(cancellationToken));

            return packages.GroupBy(p => p.PackageName).ToList();
        }

        public async Task<long> SearchListCountImplAsync(
            string filter,
            bool includePrerelease,
            bool includeSemVer2,
            string packageType = null,
            Guid? feedId = null,
            CancellationToken cancellationToken = default)
        {
            filter = filter?.ToLower();
            return await AddSearchListFilters(await GetQueryableAsync(), filter, includePrerelease, includeSemVer2, packageType, feedId)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public override async Task<IQueryable<NuGetPackage>> WithDetailsAsync()
        {
            return (await GetQueryableAsync())
                .Include(p => p.PackageTypes)
                .Include(p => p.Dependencies)
                .Include(p => p.TargetFrameworks);
        }

        private IQueryable<NuGetPackage> AddSearchListFilters(
            IQueryable<NuGetPackage> query,
            string filter,
            bool includePrerelease,
            bool includeSemVer2,
            string packageType,
            Guid? feedId)
        {
            return query.Where(p => p.IsPrerelease == includePrerelease)
                .WhereIf(!includeSemVer2, p => p.SemVerLevel != SemVerLevelEnum.SemVer2)
                .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.PackageName.ToLower().Contains(filter))
                .WhereIf(!string.IsNullOrWhiteSpace(packageType), p => p.PackageTypes.Any(t => t.Name == packageType))
                .Where(p => p.Listed == true)
                .Where(p => p.FeedId == feedId);
        }
    }
}
