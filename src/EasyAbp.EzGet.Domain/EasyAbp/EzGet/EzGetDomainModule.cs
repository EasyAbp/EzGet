﻿using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Users;
using Volo.Abp.BlobStoring;

namespace EasyAbp.EzGet
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpUsersDomainModule),
        typeof(AbpBlobStoringModule),
        typeof(EzGetDomainSharedModule)
    )]
    public class EzGetDomainModule : AbpModule
    {

    }
}
