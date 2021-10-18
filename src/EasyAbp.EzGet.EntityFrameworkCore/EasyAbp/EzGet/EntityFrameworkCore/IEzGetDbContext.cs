using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EzGet.EntityFrameworkCore
{
    [ConnectionStringName(EzGetDbProperties.ConnectionStringName)]
    public interface IEzGetDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}