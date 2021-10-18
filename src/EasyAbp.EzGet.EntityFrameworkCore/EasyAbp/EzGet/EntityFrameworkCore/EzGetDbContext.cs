using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EzGet.EntityFrameworkCore
{
    [ConnectionStringName(EzGetDbProperties.ConnectionStringName)]
    public class EzGetDbContext : AbpDbContext<EzGetDbContext>, IEzGetDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

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