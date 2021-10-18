using System;
using EasyAbp.EzGet.NuGetPackages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.EntityFrameworkCore.ValueConverters;

namespace EasyAbp.EzGet.EntityFrameworkCore
{
    public static class EzGetDbContextModelCreatingExtensions
    {
        public static void ConfigureEzGet(
            this ModelBuilder builder,
            Action<EzGetModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new EzGetModelBuilderConfigurationOptions(
                EzGetDbProperties.DbTablePrefix,
                EzGetDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<NuGetPackage>(b =>
            {
                b.ToTable(options.TablePrefix + "Packages", options.Schema);
                b.ConfigureByConvention();
                b.HasIndex(p => p.PackageName);
                b.HasIndex(p => new { p.PackageName, p.NormalizedVersion }).IsUnique();
                b.Property(p => p.PackageName).HasMaxLength(NuGetPackageConsts.MaxPackageNameLength).IsRequired();
                b.Property(p => p.Authors).HasConversion(new AbpJsonValueConverter<string[]>());
                b.Property(p => p.Description).HasMaxLength(NuGetPackageConsts.MaxDescriptionLength);
                b.Property(p => p.Downloads);
                b.Property(p => p.HasReadme);
                b.Property(p => p.HasEmbeddedIcon);
                b.Property(p => p.IsPrerelease);
                b.Property(p => p.ReleaseNotes).HasMaxLength(NuGetPackageConsts.MaxReleaseNotesLength);
                b.Property(p => p.Language).HasMaxLength(NuGetPackageConsts.MaxLanguageLength);
                b.Property(p => p.Listed);
                b.Property(p => p.MinClientVersion).HasMaxLength(NuGetPackageConsts.MaxMinClientVersionLength);
                b.Property(p => p.Published);
                b.Property(p => p.RequireLicenseAcceptance);
                b.Property(p => p.SemVerLevel);
                b.Property(p => p.Summary).HasMaxLength(NuGetPackageConsts.MaxSummaryLength);
                b.Property(p => p.Title).HasMaxLength(NuGetPackageConsts.MaxTitleLength);
                b.Property(p => p.IconUrl).HasConversion(new UriToStringConverter()).HasMaxLength(NuGetPackageConsts.MaxIconUrlLength);
                b.Property(p => p.LicenseUrl).HasConversion(new UriToStringConverter()).HasMaxLength(NuGetPackageConsts.MaxLicenseUrlLength);
                b.Property(p => p.ProjectUrl).HasConversion(new UriToStringConverter()).HasMaxLength(NuGetPackageConsts.MaxProjectUrlLength);
                b.Property(p => p.RepositoryUrl).HasConversion(new UriToStringConverter()).HasMaxLength(NuGetPackageConsts.MaxRepositoryUrlLength);
                b.Property(p => p.RepositoryType).HasMaxLength(NuGetPackageConsts.MaxRepositoryTypeLength);
                b.Property(p=>p.Tags).HasConversion(new AbpJsonValueConverter<string[]>());
                b.Property(p => p.NormalizedVersion).HasMaxLength(NuGetPackageConsts.MaxNormalizedVersionLength).IsRequired();
                b.Property(p => p.OriginalVersion).HasMaxLength(NuGetPackageConsts.MaxOriginalVersionLength);
            });

            builder.Entity<PackageDependency>(b =>
            {
                b.ToTable(options.TablePrefix + "PackageDependencies", options.Schema);
                b.ConfigureByConvention();
                b.HasIndex(p => p.PackageId);
                b.Property(p => p.DependencyPackageName).HasMaxLength(PackageDependencyConsts.MaxDependencyPackageNameLength);
                b.Property(p => p.VersionRange).HasMaxLength(PackageDependencyConsts.MaxVersionRangeLength);
                b.Property(p => p.TargetFramework).HasMaxLength(PackageDependencyConsts.MaxTargetFrameworkLength);
                b.HasOne<NuGetPackage>().WithMany(p => p.Dependencies).HasForeignKey(p => p.PackageId);
            });

            builder.Entity<PackageType>(b =>
            {
                b.ToTable(options.TablePrefix + "PackageTypes", options.Schema);
                b.ConfigureByConvention();
                b.HasIndex(p => p.PackageId);
                b.Property(p => p.Name).HasMaxLength(PackageTypeConsts.MaxNameLength);
                b.Property(p => p.Version).HasMaxLength(PackageTypeConsts.MaxVersionLength);
                b.HasOne<NuGetPackage>().WithMany(p => p.PackageTypes).HasForeignKey(p => p.PackageId);
            });

            builder.Entity<TargetFramework>(b =>
            {
                b.ToTable(options.TablePrefix + "TargetFrameworks", options.Schema);
                b.ConfigureByConvention();
                b.HasIndex(p => p.PackageId);
                b.Property(p => p.Moniker).HasMaxLength(TargetFrameworkConsts.MaxMonikerLength);
                b.HasOne<NuGetPackage>().WithMany(p => p.TargetFrameworks).HasForeignKey(p => p.PackageId);
            });
        }
    }
}