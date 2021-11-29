using EasyAbp.EzGet.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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

        public virtual async Task<Credential> GetAsync(
            Guid id,
            Guid? userId,
            bool includeDetails,
            CancellationToken cancellationToken = default)
        {
            return await (includeDetails? (await WithDetailsAsync()) : (await GetDbSetAsync()))
                .Where(p => p.Id == id)
                .WhereIf(null != userId, p => p.UserId == userId)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<Credential> FindByValueAsync(string value, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.Value == value)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<Credential>> GetListAsync(
            Guid? userId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (includeDetails ? (await GetDbSetAsync()) : (await WithDetailsAsync()))
                .WhereIf(null != userId, p => p.UserId == userId)
                .OrderBy(string.IsNullOrWhiteSpace(sorting) ? nameof(Credential.CreationTime) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            Guid? userId,
            CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync())
                .WhereIf(null != userId, p => p.UserId == userId)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public override async Task<IQueryable<Credential>> WithDetailsAsync()
        {
            return (await GetDbSetAsync()).Include(p => p.Scopes);
        }
    }
}
