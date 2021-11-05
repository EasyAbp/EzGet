using EasyAbp.EzGet.NuGet.Packages;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EzGet.NuGet.RegistrationIndexs
{
    public class RegistrationIndexBuilder : IRegistrationIndexBuilder, ITransientDependency
    {
        protected IServiceIndexUrlGenerator ServiceIndexUrlGenerator { get; }

        protected static readonly string[] DefaultTypes = new string[]
        {
            "catalog:CatalogRoot",
            "PackageRegistration",
            "catalog:Permalink"
        };

        public RegistrationIndexBuilder(IServiceIndexUrlGenerator serviceIndexUrlGenerator)
        {
            ServiceIndexUrlGenerator = serviceIndexUrlGenerator;
        }

        public virtual async Task<RegistrationIndex> BuildIndexAsync([NotNull] IReadOnlyList<NuGetPackage> nuGetPackages)
        {
            Check.NotNull(nuGetPackages, nameof(nuGetPackages));

            if (nuGetPackages.Count <= 0)
                return null;

            if (nuGetPackages.GroupBy(p => p.PackageName).Where(p => p.Count() > 1).Count() > 1)
            {
                throw new ArgumentException("Different PackageName found!");
            }

            var pacakgeName = nuGetPackages.First().PackageName;
            var sortedPackages = nuGetPackages.OrderBy(p => p.GetNuGetVersion()).ToList();

            return new RegistrationIndex(
                await ServiceIndexUrlGenerator.GetRegistrationIndexUrlAsync(sortedPackages.First().PackageName),
                DefaultTypes,
                1,
                new RegistrationPage[]
                {
                    new RegistrationPage
                    (
                        await ServiceIndexUrlGenerator.GetRegistrationIndexUrlAsync(pacakgeName),
                        sortedPackages.Count,
                        sortedPackages.First().GetNuGetVersion().ToNormalizedString().ToLowerInvariant(),
                        sortedPackages.Last().GetNuGetVersion().ToNormalizedString().ToLowerInvariant(),
                        await ToRegistrationPageItemListAsync(sortedPackages)
                    )
                });
        }

        public virtual async Task<RegistrationLeaf> BuildLeafAsync([NotNull] NuGetPackage nuGetPackage)
        {
            Check.NotNull(nuGetPackage, nameof(nuGetPackage));

            return new RegistrationLeaf(
                await ServiceIndexUrlGenerator.GetRegistrationLeafUrlAsync(nuGetPackage.PackageName, nuGetPackage.NormalizedVersion),
                nuGetPackage.Listed,
                await ServiceIndexUrlGenerator.GetPackageDownloadUrlAsync(nuGetPackage.PackageName, nuGetPackage.NormalizedVersion),
                nuGetPackage.Published,
                await ServiceIndexUrlGenerator.GetRegistrationIndexUrlAsync(nuGetPackage.PackageName));
        }

        private async Task<List<RegistrationPageItem>> ToRegistrationPageItemListAsync(IReadOnlyList<NuGetPackage> nuGetPackages)
        {
            var resultList = new List<RegistrationPageItem>(nuGetPackages.Count);

            foreach (var package in nuGetPackages)
            {
                resultList.Add(await ToRegistrationPageItemAsync(package));
            }

            return resultList;
        }

        private async Task<RegistrationPageItem> ToRegistrationPageItemAsync(NuGetPackage package)
        {
            var lowerVersion = package.GetNuGetVersion().ToNormalizedString().ToLower();
            return new RegistrationPageItem(
                await ServiceIndexUrlGenerator.GetRegistrationLeafUrlAsync(package.PackageName, lowerVersion),
                await ServiceIndexUrlGenerator.GetPackageDownloadUrlAsync(package.PackageName, lowerVersion),
                await ToNuGetPackageMetadataAsync(package));
        }

        private async Task<NuGetPackageMetadata> ToNuGetPackageMetadataAsync(NuGetPackage package)
        {
            var version = package.GetNuGetVersion().ToNormalizedString().ToLowerInvariant();

            return new NuGetPackageMetadata(
                string.Empty,
                package.PackageName,
                package.GetNuGetVersion().ToFullString(),
                string.Join(", ", package.Authors),
                package.Description,
                package.Language,
                package.HasEmbeddedIcon ? await ServiceIndexUrlGenerator.GetPacakgeIconUrlAsync(package.PackageName, version) : package.IconUrl?.AbsoluteUri,
                package.LicenseUrl?.AbsoluteUri,
                package.ProjectUrl?.AbsoluteUri,
                await ServiceIndexUrlGenerator.GetPackageDownloadUrlAsync(package.PackageName, version),
                package.Listed,
                package.MinClientVersion,
                package.Published,
                package.RequireLicenseAcceptance,
                package.Summary,
                package.Title,
                package.Tags,
                ToDependencyGroupItemList(package));
        }

        private List<DependencyGroupItem> ToDependencyGroupItemList(NuGetPackage package)
        {
            return package.Dependencies
                .GroupBy(p => p.TargetFramework)
                .Select(p => new DependencyGroupItem(
                    p.Key,
                    p.Where(d => d.Id != null && d.VersionRange != null)
                    .Select(d => new DependencyItem(d.DependencyPackageName, d.VersionRange))
                    .ToList()))
                .ToList();
        }
    }
}
