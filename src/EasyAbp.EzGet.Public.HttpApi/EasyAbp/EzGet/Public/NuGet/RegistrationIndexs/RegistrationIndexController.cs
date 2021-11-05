using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace EasyAbp.EzGet.Public.NuGet.RegistrationIndexs
{
    [RemoteService(Name = EzGetPublicRemoteServiceConsts.RemoteServiceName)]
    [Area("ezget-public")]
    [Route("api/ezget-public/registration-index")]
    public class RegistrationIndexController : EzGetPublicControllerBase, IRegistrationIndexAppService
    {
        protected IRegistrationIndexAppService RegistrationIndexAppService { get; }

        public RegistrationIndexController(IRegistrationIndexAppService registrationIndexAppService)
        {
            RegistrationIndexAppService = registrationIndexAppService;
        }

        [HttpGet]
        [Route("{packageName}")]
        public Task<RegistrationIndexDto> GetIndexAsync(string packageName)
        {
            return RegistrationIndexAppService.GetIndexAsync(packageName);
        }

        [HttpGet]
        [Route("{pacakgeName}/{version}")]
        public Task<RegistrationLeafDto> GetLeafAsync(string pacakgeName, string version)
        {
            throw new NotImplementedException();
        }
    }
}
