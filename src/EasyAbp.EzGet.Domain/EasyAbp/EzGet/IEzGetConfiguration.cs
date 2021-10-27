using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet
{
    public interface IEzGetConfiguration
    {
        Task<string> GetHostUrlAsync();
    }
}
