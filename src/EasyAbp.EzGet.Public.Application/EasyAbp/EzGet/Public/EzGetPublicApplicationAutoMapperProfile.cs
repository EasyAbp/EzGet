using AutoMapper;
using EasyAbp.EzGet.NuGet.Packages;
using EasyAbp.EzGet.NuGet.RegistrationIndexs;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet.Packages;
using EasyAbp.EzGet.Public.NuGet.RegistrationIndexs;
using EasyAbp.EzGet.Public.NuGet.ServiceIndexs;

namespace EasyAbp.EzGet.Public
{
    public class EzGetPublicApplicationAutoMapperProfile : Profile
    {
        public EzGetPublicApplicationAutoMapperProfile()
        {
            CreateMap<AlternatePackage, AlternatePackageDto>();
            CreateMap<PackageDeprecation, PackageDeprecationDto>();
            CreateMap<DependencyItem, DependencyItemDto>();
            CreateMap<DependencyGroupItem, DependencyGroupItemDto>();
            CreateMap<NuGetPackageMetadata, NuGetPackageMetadataDto>();
            CreateMap<RegistrationPageItem, RegistrationPageItemDto>();
            CreateMap<RegistrationPage, RegistrationPageDto>();
            CreateMap<RegistrationIndex, RegistrationIndexDto>();
            CreateMap<RegistrationLeaf, RegistrationLeafDto>();

            CreateMap<SearchResultVersion, SearchResultVersionDto>();
            CreateMap<SearchResultPackageType, SearchResultPackageTypeDto>();
            CreateMap<NuGetPackageSearchResult, NuGetPackageSearchResultDto>();
            CreateMap<NuGetPackageSearchListResult, NuGetPackageSearchListResultDto>();

            CreateMap<ServiceIndexResource, ServiceIndexResourceDto>();
            CreateMap<ServiceIndex, ServiceIndexDto>();
        }
    }
}