using AutoMapper;
using EasyAbp.EzGet.Credentials;
using EasyAbp.EzGet.Feeds;
using System.Collections.Generic;
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
            CreateMap<Pages.EzGet.Credentials.EditModalModel.CredentialInfoViewModel, Credentials.UpdateCredentialDto>()
                .Ignore(p => p.Scopes);
            CreateMap<Pages.EzGet.Credentials.CreateModalModel.CredentialInfoViewModel, Credentials.CreateCredentialDto>()
                .Ignore(p => p.Scopes)
                .Ignore(p => p.Expiration);

            CreateMap<FeedDto, Pages.EzGet.Feeds.EditModalModel.FeedInfoViewModel>();
            CreateMap<Pages.EzGet.Feeds.EditModalModel.FeedInfoViewModel, Feeds.UpdateFeedAdminDto>();
            CreateMap<Pages.EzGet.Feeds.CreateModalModel.FeedInfoViewModel, Feeds.CreateFeedAdminDto>();
        }
    }
}