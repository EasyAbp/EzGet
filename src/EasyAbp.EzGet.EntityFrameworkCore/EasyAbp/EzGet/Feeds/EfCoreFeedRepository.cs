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
using System.Linq.Dynamic.Core;

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

        public virtual async Task<List<Feed>> GetListAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (includeDetails ? (await GetDbSetAsync()) : (await WithDetailsAsync()))
                .WhereIf(!string.IsNullOrEmpty(filter), p => p.FeedName.Contains(filter))
                .OrderBy(string.IsNullOrWhiteSpace(sorting) ? nameof(Feed.CreationTime) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string filter = null,
            CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync())
                .WhereIf(!string.IsNullOrEmpty(filter), p => p.FeedName.Contains(filter))
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }
    }
}
