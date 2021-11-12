﻿using EasyAbp.EzGet.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Public
{
    public abstract class EzGetPublicAppServiceBase : ApplicationService
    {
        protected EzGetPublicAppServiceBase()
        {
            LocalizationResource = typeof(EzGetResource);
            ObjectMapperContext = typeof(EzGetPublicApplicationModule);
        }
    }
}
