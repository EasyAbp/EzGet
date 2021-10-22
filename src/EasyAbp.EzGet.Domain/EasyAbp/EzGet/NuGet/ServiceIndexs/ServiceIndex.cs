using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.NuGet.ServiceIndexs
{
    public class ServiceIndex
    {
        public string Version { get; set; }
        public ICollection<ServiceIndexResource> Resources { get; set; }

        public ServiceIndex(string version, ICollection<ServiceIndexResource> resources)
        {
            Version = version;
            Resources = resources;
        }
    }
}
