using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Public.NuGet.RegistrationIndexs
{
    public interface IRegistrationIndexPublicAppService : IApplicationService
    {
        Task<RegistrationIndexDto> GetIndexAsync(string packageName, string feedName);
        Task<RegistrationLeafDto> GetLeafAsync(string pacakgeName, string version, string feedName);
    }
}
