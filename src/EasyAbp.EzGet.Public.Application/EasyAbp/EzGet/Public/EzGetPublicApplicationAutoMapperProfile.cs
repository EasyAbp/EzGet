using AutoMapper;
using EasyAbp.EzGet.NuGet.RegistrationIndexs;
using EasyAbp.EzGet.Public.NuGet.RegistrationIndexs;

namespace EasyAbp.EzGet.Public
{
    public class EzGetPublicApplicationAutoMapperProfile : Profile
    {
        public EzGetPublicApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<AlternatePackage, AlternatePackageDto>();
            CreateMap<PackageDeprecation, PackageDeprecationDto>();
            CreateMap<DependencyItem, DependencyItemDto>();
            CreateMap<DependencyGroupItem, DependencyGroupItemDto>();
            CreateMap<NuGetPackageMetadata, NuGetPackageMetadataDto>();
            CreateMap<RegistrationPageItem, RegistrationPageItemDto>();
            CreateMap<RegistrationPage, RegistrationPageDto>();
            CreateMap<RegistrationIndex, RegistrationIndexDto>();
        }
    }
}