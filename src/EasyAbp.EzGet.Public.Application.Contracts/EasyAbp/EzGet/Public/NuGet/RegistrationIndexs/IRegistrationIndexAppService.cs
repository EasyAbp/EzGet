using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.EzGet.Public.NuGet.RegistrationIndexs
{
    public interface IRegistrationIndexAppService : IApplicationService
    {
        Task<RegistrationIndexDto> GetIndexAsync(string packageName);
        Task<RegistrationLeafDto> GetLeafAsync(string pacakgeName, string version);
    }
}
