using System;
using System.Collections.Generic;
using System.Linq;
using EasyAbp.EzGet.Credentials;
using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.NuGet.Packages;
using EasyAbp.EzGet.PackageRegistrations;
using EasyAbp.EzGet.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.EntityFrameworkCore.ValueConverters;
using Volo.Abp.Users.EntityFrameworkCore;

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
            ConfigurePackageRegistrations(builder, options);
            ConfigureNuGetPackages(builder, options);
            ConfigureCredentials(builder, options);
            ConfigureFeeds(builder, options);
            ConfigureUsers(builder, options);
        }

        private static void ConfigurePackageRegistrations(ModelBuilder builder, EzGetModelBuilderConfigurationOptions options)
        {
            builder.Entity<PackageRegistration>(b =>
            {
                b.ToTable(options.TablePrefix + "PackageRegistrations", options.Schema);
                b.ConfigureByConvention();
                b.HasIndex(p => p.FeedId);
                b.HasIndex(p => new { p.PackageName, p.PackageType, p.FeedId }).IsUnique();
                b.Property(p => p.PackageName).HasMaxLength(PackageRegistrationConsts.MaxPackageNameLength).IsRequired();
                b.Property(p => p.PackageType).HasMaxLength(PackageRegistrationConsts.MaxPackageTypeLength).IsRequired();
                b.Property(p => p.Description).HasMaxLength(PackageRegistrationConsts.MaxDescriptionLength);
                b.Property(p => p.LastVersion).HasMaxLength(PackageRegistrationConsts.MaxLastVersionLength);
                b.Property(p => p.Size);
                b.Property(p => p.DownloadCount);
                b.HasMany(p => p.PackageRegistrationOwners).WithOne().HasForeignKey(p => p.PackageRegistrationId);
            });

            builder.Entity<PackageRegistrationOwner>(b =>
            {
                b.ToTable(options.TablePrefix + "PackageRegistrationOwners", options.Schema);
                b.ConfigureByConvention();
                b.HasKey(p => new { p.PackageRegistrationId, p.UserId });
            });
        }

        private static void ConfigureNuGetPackages(ModelBuilder builder, EzGetModelBuilderConfigurationOptions options)
        {
            builder.Entity<NuGetPackage>(b =>
            {
                b.ToTable(options.TablePrefix + "NuGetPackages", options.Schema);
                b.ConfigureByConvention();
                b.HasIndex(p => p.PackageRegistrationId);
                b.HasIndex(p => p.PackageName);
                b.HasIndex(p => new { p.PackageName, p.NormalizedVersion, p.PackageRegistrationId }).IsUnique();
                b.Property(p => p.PackageName).HasMaxLength(NuGetPackageConsts.MaxPackageNameLength).IsRequired();
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
                b.Property(p => p.NormalizedVersion).HasMaxLength(NuGetPackageConsts.MaxNormalizedVersionLength).IsRequired();
                b.Property(p => p.OriginalVersion).HasMaxLength(NuGetPackageConsts.MaxOriginalVersionLength);

                var valueComparer = new ValueComparer<string[]>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c);

                b.Property(p => p.Authors).HasConversion(new AbpJsonValueConverter<string[]>()).Metadata.SetValueComparer(valueComparer);
                b.Property(p => p.Tags).HasConversion(new AbpJsonValueConverter<string[]>()).Metadata.SetValueComparer(valueComparer);
            });

            builder.Entity<PackageDependency>(b =>
            {
                b.ToTable(options.TablePrefix + "NuGetPackageDependencies", options.Schema);
                b.ConfigureByConvention();
                b.HasIndex(p => p.PackageId);
                b.Property(p => p.DependencyPackageName).HasMaxLength(PackageDependencyConsts.MaxDependencyPackageNameLength);
                b.Property(p => p.VersionRange).HasMaxLength(PackageDependencyConsts.MaxVersionRangeLength);
                b.Property(p => p.TargetFramework).HasMaxLength(PackageDependencyConsts.MaxTargetFrameworkLength);
                b.HasOne<NuGetPackage>().WithMany(p => p.Dependencies).HasForeignKey(p => p.PackageId);
            });

            builder.Entity<PackageType>(b =>
            {
                b.ToTable(options.TablePrefix + "NuGetPackageTypes", options.Schema);
                b.ConfigureByConvention();
                b.HasIndex(p => p.PackageId);
                b.Property(p => p.Name).HasMaxLength(PackageTypeConsts.MaxNameLength);
                b.Property(p => p.Version).HasMaxLength(PackageTypeConsts.MaxVersionLength);
                b.HasOne<NuGetPackage>().WithMany(p => p.PackageTypes).HasForeignKey(p => p.PackageId);
            });

            builder.Entity<TargetFramework>(b =>
            {
                b.ToTable(options.TablePrefix + "NuGetTargetFrameworks", options.Schema);
                b.ConfigureByConvention();
                b.HasIndex(p => p.PackageId);
                b.Property(p => p.Moniker).HasMaxLength(TargetFrameworkConsts.MaxMonikerLength);
                b.HasOne<NuGetPackage>().WithMany(p => p.TargetFrameworks).HasForeignKey(p => p.PackageId);
            });
        }

        private static void ConfigureCredentials(ModelBuilder builder, EzGetModelBuilderConfigurationOptions options)
        {
            builder.Entity<Credential>(b =>
            {
                b.ToTable(options.TablePrefix + "Credentials", options.Schema);
                b.ConfigureByConvention();
                b.HasIndex(p => p.Value);
                b.Property(p => p.UserId).IsRequired();
                b.Property(p => p.Value).IsRequired().HasMaxLength(CredentialConsts.MaxValueLength);
                b.Property(p => p.Description).HasMaxLength(CredentialConsts.MaxDescriptionLength);
                b.Property(p => p.Expires);
                b.Property(p => p.GlobPattern).HasMaxLength(CredentialConsts.MaxGlobPatternLength);
                b.HasMany(p => p.Scopes).WithOne().HasForeignKey(p => p.CredentialId);
            });

            builder.Entity<CredentialScope>(b =>
            {
                b.ToTable(options.TablePrefix + "CredentialScopes", options.Schema);
                b.ConfigureByConvention();
                b.HasKey(p => new { p.CredentialId, p.AllowAction });
                b.Property(p => p.CredentialId);
                b.Property(p => p.AllowAction);
            });
        }

        private static void ConfigureFeeds(ModelBuilder builder, EzGetModelBuilderConfigurationOptions options)
        {
            builder.Entity<Feed>(b =>
            {
                b.ToTable(options.TablePrefix + "Feeds", options.Schema);
                b.ConfigureByConvention();
                b.HasIndex(p => p.FeedName);
                b.Property(p => p.FeedName).HasMaxLength(FeedConsts.MaxFeedNameLenght);
                b.Property(p => p.Description).HasMaxLength(FeedConsts.MaxDescriptionLength);
                b.Property(p => p.FeedType);
                b.HasMany(p => p.FeedCredentials).WithOne().HasForeignKey(p => p.FeedId);
            });

            builder.Entity<FeedCredential>(b =>
            {
                b.ToTable(options.TablePrefix + "FeedCredentials", options.Schema);
                b.ConfigureByConvention();
                b.HasKey(p => new { p.FeedId, p.CredentialId });
            });

            builder.Entity<FeedPermissionGrant>(b =>
            {
                b.ToTable(options.TablePrefix + "FeedPermissionGrants", options.Schema);
                b.ConfigureByConvention();
                b.HasKey(p => new { p.FeedId, p.ProviderName, p.ProviderKey });
                b.Property(p => p.ProviderName).HasMaxLength(FeedPermissionGrantConsts.MaxProviderNameLenght);
                b.Property(p => p.ProviderKey).HasMaxLength(FeedPermissionGrantConsts.MaxProviderKeyLenght);
            });
        }

        private static void ConfigureUsers(ModelBuilder builder, EzGetModelBuilderConfigurationOptions options)
        {
            builder.Entity<EzGetUser>(b =>
            {
                b.ToTable(options.TablePrefix + "Users", options.Schema);
                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                b.HasIndex(x => new { x.TenantId, x.UserName });
                b.HasIndex(x => new { x.TenantId, x.Email });

                b.ApplyObjectExtensionMappings();
            });
        }
    }
}