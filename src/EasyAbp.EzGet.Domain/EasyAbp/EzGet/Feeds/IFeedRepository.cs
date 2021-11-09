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
        Task<Feed> FindByNameAsync(string name, CancellationToken cancellationToken);
    }
}
