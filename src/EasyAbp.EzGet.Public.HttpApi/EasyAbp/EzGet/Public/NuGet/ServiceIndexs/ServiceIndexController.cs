using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace EasyAbp.EzGet.Public.NuGet.ServiceIndexs
{
    [RemoteService(Name = EzGetPublicRemoteServiceConsts.RemoteServiceName)]
    [Area("ezget-public")]
    [Route("api/ezget-public/service-index")]
    public class ServiceIndexController : EzGetPublicControllerBase, IServiceIndexAppService
    {
        protected IServiceIndexAppService ServiceIndexAppService { get; }

        public ServiceIndexController(IServiceIndexAppService serviceIndexAppService)
        {
            ServiceIndexAppService = serviceIndexAppService;
        }

        [HttpGet]
        public Task<ServiceIndexDto> GetAsync()
        {
            return ServiceIndexAppService.GetAsync();
        }
    }
}
