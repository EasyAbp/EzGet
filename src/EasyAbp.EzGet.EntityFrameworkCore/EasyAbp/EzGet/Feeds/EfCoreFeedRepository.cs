using EasyAbp.EzGet.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EzGet.Feeds
{
    public class EfCoreFeedRepository : EfCoreRepository<IEzGetDbContext, Feed, Guid>, IFeedRepository
    {
        public EfCoreFeedRepository(IDbContextProvider<IEzGetDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<Feed> FindByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await (await GetQueryableAsync())
                .Where(p => p.FeedName == name)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }
    }
}
