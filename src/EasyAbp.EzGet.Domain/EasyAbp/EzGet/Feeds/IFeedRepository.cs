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
        Task<bool> ExistedAsync(string name, CancellationToken cancellationToken = default);

        Task<Feed> FindByNameAsync(
            string name,
            Guid? userId = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default);

        Task<List<Feed>> GetListAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            string feedName = null,
            Guid? userId = null,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string filter = null,
            string feedName = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default);
    }
}
