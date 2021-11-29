using EasyAbp.EzGet.Credentials;
using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.NuGet.Packages;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EzGet.EntityFrameworkCore
{
    [ConnectionStringName(EzGetDbProperties.ConnectionStringName)]
    public class EzGetDbContext : AbpDbContext<EzGetDbContext>, IEzGetDbContext
    {
        public DbSet<NuGetPackage> NuGetPackages { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Feed> Feeds { get; set; }

        public EzGetDbContext(DbContextOptions<EzGetDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureEzGet();
        }
    }
}