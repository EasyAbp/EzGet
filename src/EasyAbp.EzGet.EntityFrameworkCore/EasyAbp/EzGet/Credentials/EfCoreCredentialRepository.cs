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

namespace EasyAbp.EzGet.Credentials
{
    public class EfCoreCredentialRepository : EfCoreRepository<IEzGetDbContext, Credential, Guid>, ICredentialRepository
    {
        public EfCoreCredentialRepository(IDbContextProvider<IEzGetDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Credential> FindByValueAsync(string value, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.Value == value)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }
    }
}
