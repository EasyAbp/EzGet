using EasyAbp.EzGet.Credentials;
using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.NuGet.Packages;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EzGet.EntityFrameworkCore
{
    [ConnectionStringName(EzGetDbProperties.ConnectionStringName)]
    public interface IEzGetDbContext : IEfCoreDbContext
    {
        DbSet<NuGetPackage> NuGetPackages { get; }
        DbSet<Credential> Credentials { get; }
        DbSet<Feed> Feeds { get; }
    }
}