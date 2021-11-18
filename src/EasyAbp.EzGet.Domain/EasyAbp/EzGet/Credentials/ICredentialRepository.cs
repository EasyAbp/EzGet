using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EzGet.Credentials
{
    public interface ICredentialRepository : IBasicRepository<Credential, Guid>
    {
        Task<Credential> GetAsync(Guid id, Guid? userId, CancellationToken cancellationToken = default);
        Task<Credential> FindByValueAsync(string value, CancellationToken cancellationToken = default);
        Task<List<Credential>> GetListAsync(
            Guid? userId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);
        Task<long> GetCountAsync(
            Guid? userId,
            CancellationToken cancellationToken = default);
    }
}
