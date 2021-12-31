using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace EasyAbp.EzGet.Public.NuGet.ServiceIndexs
{
    [Area(EzGetPublicRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = EzGetPublicRemoteServiceConsts.RemoteServiceName)]
    [Route("api/ez-get-public/service-index")]
    public class ServiceIndexPublicController : EzGetPublicControllerBase, IServiceIndexPublicAppService
    {
        protected IServiceIndexPublicAppService ServiceIndexAppService { get; }

        public ServiceIndexPublicController(IServiceIndexPublicAppService serviceIndexAppService)
        {
            ServiceIndexAppService = serviceIndexAppService;
        }

        [HttpGet]
        public Task<ServiceIndexDto> GetAsync([FromQuery] string feedName)
        {
            return ServiceIndexAppService.GetAsync(feedName);
        }
    }
}
