using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EzGet.EntityFrameworkCore
{
    public class EzGetHttpApiHostMigrationsDbContext : AbpDbContext<EzGetHttpApiHostMigrationsDbContext>
    {
        public EzGetHttpApiHostMigrationsDbContext(DbContextOptions<EzGetHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureEzGet();
        }
    }
}
