using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet.ServiceIndexs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/service-index
    [Route(ServiceIndexUrlConsts.ServiceIndexUrl)]
    public class ServiceIndexApiController : ControllerBase
    {
        protected IServiceIndexAppService ServiceIndexAppService { get; }

        public ServiceIndexApiController(IServiceIndexAppService serviceIndexAppService)
        {
            ServiceIndexAppService = serviceIndexAppService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync()
        {
            var serviceIndex = await ServiceIndexAppService.GetAsync();
            return new JsonResult(serviceIndex);
        }
    }
}
