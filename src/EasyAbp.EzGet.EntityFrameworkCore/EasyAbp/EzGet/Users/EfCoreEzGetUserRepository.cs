using EasyAbp.EzGet.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq.Dynamic.Core;
using System.Linq;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EzGet.Users
{
    public class EfCoreEzGetUserRepository : EfCoreUserRepositoryBase<IEzGetDbContext, EzGetUser>, IEzGetUserRepository
    {
        public EfCoreEzGetUserRepository(IDbContextProvider<IEzGetDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<List<EzGetUser>> GetListAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            string userName = null,
            string phoneNumber = null,
            string emailAddress = null,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.UserName.Contains(filter) ||
                        u.Email.Contains(filter) ||
                        (u.Name != null && u.Name.Contains(filter)) ||
                        (u.Surname != null && u.Surname.Contains(filter)) ||
                        (u.PhoneNumber != null && u.PhoneNumber.Contains(filter))
                )
                .WhereIf(!string.IsNullOrWhiteSpace(userName), x => x.UserName == userName)
                .WhereIf(!string.IsNullOrWhiteSpace(phoneNumber), x => x.PhoneNumber == phoneNumber)
                .WhereIf(!string.IsNullOrWhiteSpace(emailAddress), x => x.Email == emailAddress)
                .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(EzGetUser.UserName) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string filter = null,
            string userName = null,
            string phoneNumber = null,
            string emailAddress = null,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.UserName.Contains(filter) ||
                        u.Email.Contains(filter) ||
                        (u.Name != null && u.Name.Contains(filter)) ||
                        (u.Surname != null && u.Surname.Contains(filter)) ||
                        (u.PhoneNumber != null && u.PhoneNumber.Contains(filter))
                )
                .WhereIf(!string.IsNullOrWhiteSpace(userName), x => x.UserName == userName)
                .WhereIf(!string.IsNullOrWhiteSpace(phoneNumber), x => x.PhoneNumber == phoneNumber)
                .WhereIf(!string.IsNullOrWhiteSpace(emailAddress), x => x.Email == emailAddress)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }
    }
}
