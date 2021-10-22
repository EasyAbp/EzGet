using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Public.NuGet.ServiceIndexs
{
    public class ServiceIndexDto
    {
        public string Version { get; set; }
        public IEnumerable<ServiceIndexResourceDto> Resources { get; set; }
    }
}
