using EasyAbp.EzGet.EntityFrameworkCore;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EzGet.NuGet.Packages
{
    //TODO: Implementation using ES
    public class EfCoreNuGetPackageSearcher : EfCoreRepository<IEzGetDbContext, NuGetPackage, Guid>, INuGetPackageSearcher, ITransientDependency
    {
        protected IServiceIndexUrlGenerator ServiceIndexUrlGenerator { get; }

        public EfCoreNuGetPackageSearcher(
            IDbContextProvider<IEzGetDbContext> dbContextProvider,
            IServiceIndexUrlGenerator serviceIndexUrlGenerator)
            : base(dbContextProvider)
        {
            ServiceIndexUrlGenerator = serviceIndexUrlGenerator;
        }

        public virtual async Task<NuGetPackageSearchListResult> SearchListAsync(
            int skip,
            int take,
            string filter,
            bool includePrerelease,
            bool includeSemVer2,
            string packageType = null,
            CancellationToken cancellationToken = default)
        {
            var count = await SearchListCountImplAsync(
                filter,
                includePrerelease,
                includeSemVer2,
                packageType,
                cancellationToken);

            var packageResultList = new List<NuGetPackageSearchResult>();
            var packages = await SearchListImplAsync(
                skip,
                take,
                filter,
                includePrerelease,
                includeSemVer2,
                packageType,
                cancellationToken);

            foreach (var package in packages)
            {
                var versions = package.OrderByDescending(p => p.GetNuGetVersion()).ToList();
                var latest = versions.First();
                var iconUrl = latest.HasEmbeddedIcon ?
                    await ServiceIndexUrlGenerator.GetPacakgeIconUrlAsync(latest.PackageName, latest.GetNuGetVersion().ToNormalizedString().ToLowerInvariant()) :
                    latest.IconUrl.AbsoluteUri;

                var packageResult = new NuGetPackageSearchResult(
                    latest.PackageName,
                    latest.GetNuGetVersion().ToFullString(),
                    latest.Description,
                    latest.Authors,
                    iconUrl,
                    latest.LicenseUrl.AbsoluteUri,
                    latest.ProjectUrl.AbsoluteUri,
                    await ServiceIndexUrlGenerator.GetRegistrationIndexUrlAsync(latest.PackageName),
                    latest.Summary,
                    latest.Tags,
                    latest.Downloads,
                    GetSearchResultPackageTypes(versions),
                    await GetSearchResultVersionsAsync(versions));

                packageResultList.Add(packageResult);
            }

            return new NuGetPackageSearchListResult(count, packageResultList);
        }

        private async Task<List<SearchResultVersion>> GetSearchResultVersionsAsync(List<NuGetPackage> packages)
        {
            var result = new List<SearchResultVersion>();

            foreach (var package in packages)
            {
                result.Add(new SearchResultVersion(
                    await ServiceIndexUrlGenerator.GetRegistrationLeafUrlAsync(package.PackageName, package.GetNuGetVersion().ToNormalizedString().ToLowerInvariant()),
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
            string packageType = null,
            CancellationToken cancellationToken = default)
        {
            filter = filter.ToLower();

            var query = AddSearchListFilters(await GetQueryableAsync(), filter, includePrerelease, includeSemVer2, packageType)
                .Select(p => p.PackageName)
                .OrderBy(p => p)
                .Skip(skip)
                .Take(take);

            var packageNames = await query.ToListAsync(GetCancellationToken(cancellationToken));

            var packages = await AddSearchListFilters(await WithDetailsAsync(), filter, includePrerelease, includeSemVer2, packageType)
                .Where(p => packageNames.Contains(p.PackageName))
                .ToListAsync(GetCancellationToken(cancellationToken));

            return packages.GroupBy(p => p.PackageName).ToList();
        }

        public async Task<long> SearchListCountImplAsync(
            string filter,
            bool includePrerelease,
            bool includeSemVer2,
            string packageType = null,
            CancellationToken cancellationToken = default)
        {
            filter = filter.ToLower();
            return await AddSearchListFilters(await GetQueryableAsync(), filter, includePrerelease, includeSemVer2, packageType)
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
            string packageType = null)
        {
            return query.Where(p => p.IsPrerelease == includePrerelease)
                .WhereIf(!includeSemVer2, p => p.SemVerLevel != SemVerLevelEnum.SemVer2)
                .WhereIf(includeSemVer2, p => p.SemVerLevel == SemVerLevelEnum.SemVer2)
                .WhereIf(string.IsNullOrWhiteSpace(filter), p => p.PackageName.ToLower().Contains(filter))
                .WhereIf(string.IsNullOrWhiteSpace(packageType), p => p.PackageTypes.Any(t => t.Name == packageType))
                .Where(p => p.Listed == true);
        }
    }
}
