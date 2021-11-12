using AutoMapper;
using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.NuGet.Packages;
using System.Linq;
using Volo.Abp.AutoMapper;

namespace EasyAbp.EzGet
{
    public class EzGetCommonApplicationAutoMapperProfile : Profile
    {
        public EzGetCommonApplicationAutoMapperProfile()
        {
            CreateNuGet();
            CreateFeeds();
        }

        private void CreateNuGet()
        {
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

        private void CreateFeeds()
        {
            CreateMap<Feed, FeedDto>()
                .ForMember(
                p => p.CredentialIds,
                d => d.MapFrom(s => s.FeedCredentials.Select(f => f.CredentialId)));
        }
    }
}