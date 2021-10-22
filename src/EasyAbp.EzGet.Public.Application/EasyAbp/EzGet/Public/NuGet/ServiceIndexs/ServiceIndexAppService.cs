using EasyAbp.EzGet.NuGet.ServiceIndexs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet.ServiceIndexs
{
    public class ServiceIndexAppService : EzGetPublicAppServiceBase, IServiceIndexAppService
    {
        protected IServiceIndexManager ServiceIndexManager { get; }

        public ServiceIndexAppService(IServiceIndexManager serviceIndexManager)
        {
            ServiceIndexManager = serviceIndexManager;
        }

        public virtual async Task<ServiceIndexDto> GetAsync()
        {
            return ObjectMapper.Map<ServiceIndex, ServiceIndexDto>(await ServiceIndexManager.GetAsync());
        }
    }
}
