using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet.ServiceIndexs
{
    public class ServiceIndexPublicAppService : EzGetPublicNuGetAppServiceBase, IServiceIndexPublicAppService
    {
        protected IServiceIndexManager ServiceIndexManager { get; }

        public ServiceIndexPublicAppService(
            IServiceIndexManager serviceIndexManager,
            IFeedStore feedStore) : base(feedStore)
        {
            ServiceIndexManager = serviceIndexManager;
        }

        [AllowAnonymousIfFeedPublic]
        [Authorize(EzGetPublicPermissions.ServiceIndexs.Default)]
        public virtual async Task<ServiceIndexDto> GetAsync(string feedName)
        {
            return ObjectMapper.Map<ServiceIndex, ServiceIndexDto>(await ServiceIndexManager.GetAsync(feedName));
        }
    }
}
