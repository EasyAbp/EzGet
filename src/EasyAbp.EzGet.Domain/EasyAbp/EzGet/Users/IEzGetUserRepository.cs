using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace EasyAbp.EzGet.Users
{
    public interface IEzGetUserRepository : IUserRepository<EzGetUser>
    {
        Task<List<EzGetUser>> GetListAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            string userName = null,
            string phoneNumber = null,
            string emailAddress = null,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string filter = null,
            string userName = null,
            string phoneNumber = null,
            string emailAddress = null,
            CancellationToken cancellationToken = default);
    }
}
