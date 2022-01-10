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

        public virtual async Task<NuGetPackageSearchNameListResult> SearchNameListAsync(
            int skip,
            int take,
            string filter,
            bool includePrerelease,
            bool includeSemVer2,
            string packageType = null,
            string feedName = null,
            CancellationToken cancellationToken = default)
        {
            var feedId = await GetFeedIdAsync(feedName);

            var count = await QueryCountAsync(
                filter,
                includePrerelease,
                includeSemVer2,
                packageType,
                feedId,
                cancellationToken);

            var names = await QueryNamesAsync(
                filter,
                includePrerelease,
                includeSemVer2,
                packageType,
                feedId,
                cancellationToken);

            return new NuGetPackageSearchNameListResult(count, names);
        }

        public virtual async Task<NuGetPackageSearchPackageListResult> SearchPackageListAsync(
            int skip,
            int take,
            string filter,
            bool includePrerelease,
            bool includeSemVer2,
            string packageType = null,
            string feedName = null,
            CancellationToken cancellationToken = default)
        {
            var feedId = await GetFeedIdAsync(feedName);

            var count = await QueryCountAsync(
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

            return new NuGetPackageSearchPackageListResult(count, packageResultList);
        }


        public override async Task<IQueryable<NuGetPackage>> WithDetailsAsync()
        {
            return (await GetQueryableAsync())
                .Include(p => p.PackageTypes)
                .Include(p => p.Dependencies)
                .Include(p => p.TargetFrameworks);
        }

        protected virtual IQueryable<NuGetPackage> AddSearchListFilters(
            IQueryable<NuGetPackage> query,
            string filter,
            bool includePrerelease,
            bool includeSemVer2,
            string packageType)
        {
            return query.Where(p => p.IsPrerelease == includePrerelease)
                .WhereIf(!includeSemVer2, p => p.SemVerLevel != SemVerLevelEnum.SemVer2)
                .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.PackageName.ToLower().Contains(filter))
                .WhereIf(!string.IsNullOrWhiteSpace(packageType), p => p.PackageTypes.Any(t => t.Name == packageType))
                .Where(p => p.Listed == true);
        }

        private async Task<Guid?> GetFeedIdAsync(string feedName)
        {
            Guid? feedId = null;

            if (!string.IsNullOrEmpty(feedName))
            {
                feedId = (await FeedStore.GetAsync(feedName)).Id;
            }

            return feedId;
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

            var query = AddSearchListFilters(await GetFeedQueryableAsync(feedId, false), filter, includePrerelease, includeSemVer2, packageType)
                .Select(p => p.PackageName)
                .OrderBy(p => p)
                .Skip(skip)
                .Take(take);

            var packageNames = await query.ToListAsync(GetCancellationToken(cancellationToken));

            var packages = await AddSearchListFilters(await GetFeedQueryableAsync(feedId, true), filter, includePrerelease, includeSemVer2, packageType)
                .Where(p => packageNames.Contains(p.PackageName))
                .ToListAsync(GetCancellationToken(cancellationToken));

            return packages.GroupBy(p => p.PackageName).ToList();
        }

        private async Task<List<string>> QueryNamesAsync(
            string filter,
            bool includePrerelease,
            bool includeSemVer2,
            string packageType,
            Guid? feedId,
            CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            var query = AddSearchListFilters(dbContext.NuGetPackages, filter, includePrerelease, includeSemVer2, packageType)
                .Select(p => p.PackageName);

            return await dbContext.PackageRegistrations
                .Where(p => query.Contains(p.PackageName))
                .Where(p => p.FeedId == feedId)
                .Select(p => p.PackageName)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        private async Task<long> QueryCountAsync(
            string filter,
            bool includePrerelease,
            bool includeSemVer2,
            string packageType,
            Guid? feedId,
            CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            var query = AddSearchListFilters(dbContext.NuGetPackages, filter, includePrerelease, includeSemVer2, packageType)
                .Select(p => p.PackageName);

            return await dbContext.PackageRegistrations
                .Where(p => query.Contains(p.PackageName))
                .Where(p => p.FeedId == feedId)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        private async Task<IQueryable<NuGetPackage>> GetFeedQueryableAsync(Guid? feedId, bool includeDetails)
        {
            var dbContext = await GetDbContextAsync();
            return from package in includeDetails ? (await WithDetailsAsync()) : (await GetQueryableAsync())
                   join registration in dbContext.PackageRegistrations on package.PackageRegistrationId equals registration.Id
                   where registration.FeedId == feedId
                   select package;
        }
    }
}
