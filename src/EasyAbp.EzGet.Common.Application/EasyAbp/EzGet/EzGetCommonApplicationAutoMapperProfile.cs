using AutoMapper;
using EasyAbp.EzGet.NuGetPackages;
using Volo.Abp.AutoMapper;

namespace EasyAbp.EzGet
{
    public class EzGetCommonApplicationAutoMapperProfile : Profile
    {
        public EzGetCommonApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<TargetFramework, TargetFrameworkDto>();
            CreateMap<PackageType, PackageTypeDto>();
            CreateMap<PackageDependency, PackageDependencyDto>();

            CreateMap<NuGetPackage, NuGetPackageDto>()
                .ForMember(
                p => p.IconUrl,
                d => d.MapFrom(s => s.IconUrl.AbsoluteUri))
                .ForMember(
                p => p.LicenseUrl,
                d => d.MapFrom(s => s.LicenseUrl.AbsoluteUri))
                .ForMember(
                p => p.ProjectUrl,
                d => d.MapFrom(s => s.ProjectUrl.AbsoluteUri))
                .ForMember(
                p => p.RepositoryUrl,
                d => d.MapFrom(s => s.RepositoryUrl.AbsoluteUri));

        }
    }
}