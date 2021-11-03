using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet.RegistrationIndexs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EzGet.Public.NuGet
{
    //See: https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource
    [Route(ServiceIndexUrlConsts.RegistrationsBaseUrlUrl)]
    public class RegistrationIndexApiController : AbpController
    {
        private readonly IRegistrationIndexAppService _registrationIndexAppService;

        public RegistrationIndexApiController(IRegistrationIndexAppService registrationIndexAppService)
        {
            _registrationIndexAppService = registrationIndexAppService;
        }

        [HttpGet]
        [Route("{id}/index.json")]
        public virtual Task<RegistrationIndexDto> GetAsync(string id)
        {
            return _registrationIndexAppService.GetAsync(id);
        }
    }
}
