using AutoMapper;
using EasyAbp.EzGet.Credentials;
using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.NuGet.Packages;
using System.Linq;
using System;
using Volo.Abp.AutoMapper;

namespace EasyAbp.EzGet
{
    public class EzGetCommonApplicationAutoMapperProfile : Profile
    {
        public EzGetCommonApplicationAutoMapperProfile()
        {
            CreateNuGet();
            CreateFeeds();
            CreateCredentials();
        }

        private void CreateNuGet()
        {
            CreateMap<TargetFramework, TargetFrameworkDto>();
            CreateMap<PackageType, PackageTypeDto>();
            CreateMap<PackageDependency, PackageDependencyDto>();

            CreateMap<NuGetPackage, NuGetPackageDto>()
                .ForMember(
                p => p.IconUrl,
                d => d.MapFrom(s => s.IconUrl.GetAbsoluteUriOrEmpty()))
                .ForMember(
                p => p.LicenseUrl,
                d => d.MapFrom(s => s.LicenseUrl.GetAbsoluteUriOrEmpty()))
                .ForMember(
                p => p.ProjectUrl,
                d => d.MapFrom(s => s.ProjectUrl.GetAbsoluteUriOrEmpty()))
                .ForMember(
                p => p.RepositoryUrl,
                d => d.MapFrom(s => s.RepositoryUrl.GetAbsoluteUriOrEmpty()));
        }

        private void CreateFeeds()
        {
            CreateMap<Feed, FeedDto>()
                .ForMember(
                p => p.CredentialIds,
                d => d.MapFrom(s => s.FeedCredentials.Select(f => f.CredentialId)));
        }

        private void CreateCredentials()
        {
            CreateMap<CredentialScope, CredentialScopeDto>();
            CreateMap<Credential, CredentialDto>();
        }
    }
}