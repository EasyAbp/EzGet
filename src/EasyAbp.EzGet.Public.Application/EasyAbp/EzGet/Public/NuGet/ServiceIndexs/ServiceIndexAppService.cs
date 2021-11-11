using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet.ServiceIndexs
{
    public class ServiceIndexAppService : EzGetPublicNuGetAppServiceBase, IServiceIndexAppService
    {
        protected IServiceIndexManager ServiceIndexManager { get; }

        public ServiceIndexAppService(
            IServiceIndexManager serviceIndexManager,
            IFeedStore feedStore) : base(feedStore)
        {
            ServiceIndexManager = serviceIndexManager;
        }

        public virtual async Task<ServiceIndexDto> GetAsync(string feedName)
        {
            return ObjectMapper.Map<ServiceIndex, ServiceIndexDto>(await ServiceIndexManager.GetAsync(feedName));
        }
    }
}
