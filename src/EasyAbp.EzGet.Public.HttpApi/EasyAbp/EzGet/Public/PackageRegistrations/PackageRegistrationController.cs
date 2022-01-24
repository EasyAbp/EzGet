using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Public.PackageRegistrations
{
    [Area(EzGetPublicRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = EzGetPublicRemoteServiceConsts.RemoteServiceName)]
    [Route("api/ez-get-public/package-registrations")]
    public class PackageRegistrationController : EzGetPublicControllerBase, IPackageRegistrationPublicAppService
    {
        protected IPackageRegistrationPublicAppService PackageRegistrationPublicAppService { get; }

        public PackageRegistrationController(IPackageRegistrationPublicAppService packageRegistrationPublicAppService)
        {
            PackageRegistrationPublicAppService = packageRegistrationPublicAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<PackageRegistrationDto> GetAsync(Guid id)
        {
            return PackageRegistrationPublicAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<PackageRegistrationDto>> GetListAsync(GetPackageRegistrationsInput input)
        {
            return PackageRegistrationPublicAppService.GetListAsync(input);
        }
    }
}
