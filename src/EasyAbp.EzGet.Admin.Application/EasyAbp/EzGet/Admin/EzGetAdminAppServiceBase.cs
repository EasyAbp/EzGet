using EasyAbp.EzGet.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Admin
{
    public abstract class EzGetAdminAppServiceBase : ApplicationService
    {
        protected EzGetAdminAppServiceBase()
        {
            LocalizationResource = typeof(EzGetResource);
            ObjectMapperContext = typeof(EzGetAdminApplicationModule);
        }
    }
}
