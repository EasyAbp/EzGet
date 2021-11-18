using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EzGet.Feeds
{
    public interface IFeedRepository : IBasicRepository<Feed, Guid>
    {
        Task<Feed> FindByNameAsync(string name, Guid? userId = null, CancellationToken cancellationToken = default);

        Task<List<Feed>> GetListAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            Guid? userId = null,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string filter = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default);
    }
}
