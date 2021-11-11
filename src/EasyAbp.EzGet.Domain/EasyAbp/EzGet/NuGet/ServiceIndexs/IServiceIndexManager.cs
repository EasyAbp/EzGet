using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.NuGet.ServiceIndexs
{
    public interface IServiceIndexManager
    {
        Task<ServiceIndex> GetAsync(string feedName);
    }
}
