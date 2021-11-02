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
        Task<Credential> FindByValueAsync(string value, CancellationToken cancellationToken = default);

    }
}
