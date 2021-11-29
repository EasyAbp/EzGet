using AutoMapper;
using EasyAbp.EzGet.Credentials;
using Volo.Abp.AutoMapper;

namespace EasyAbp.EzGet.Admin.Web
{
    public class EzGetAdminWebAutoMapperProfile : Profile
    {
        public EzGetAdminWebAutoMapperProfile()
        {
            CreateMap<CredentialDto, Pages.EzGet.Credentials.EditModalModel.CredentialInfoViewModel>()
                .Ignore(p => p.Read)
                .Ignore(p => p.Write);
        }
    }
}