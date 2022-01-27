using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost]
        [Route("{id}/owners")]
        public Task AddOwnerAsync(Guid id, AddOwnerDto input)
        {
            return PackageRegistrationPublicAppService.AddOwnerAsync(id, input);
        }
        
        [HttpDelete]
        [Route("{id}/owners/{userId}")]
        public Task RemoveOwnerAsync(Guid id, Guid userId)
        {
            return PackageRegistrationPublicAppService.RemoveOwnerAsync(id, userId);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id, DeletePackageRegistrationInput input)
        {
            return PackageRegistrationPublicAppService.DeleteAsync(id, input);
        }
    }
}
