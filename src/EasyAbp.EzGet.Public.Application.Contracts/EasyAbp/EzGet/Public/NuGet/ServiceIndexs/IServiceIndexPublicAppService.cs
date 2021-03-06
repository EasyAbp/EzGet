using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Public.NuGet.ServiceIndexs
{
    public interface IServiceIndexPublicAppService : IApplicationService
    {
        Task<ServiceIndexDto> GetAsync(string feedName);
    }
}
