using AutoMapper;
using EasyAbp.EzGet.Admin.Users;
using EasyAbp.EzGet.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Admin
{
    public class EzGetAdminApplicationAutoMapperProfile : Profile
    {
        public EzGetAdminApplicationAutoMapperProfile()
        {
            CreateMap<EzGetUser, EzGetUserDto>();
        }
    }
}
