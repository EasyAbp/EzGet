using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace EasyAbp.EzGet.NuGet.ServiceIndexs
{
    public class ServiceIndexResource
    {
        public string ResourceUrl { get; }
        public string Type { get; }
        public string Comment { get; }

        public ServiceIndexResource(string resourceUrl, string type, string comment = null)
        {
            ResourceUrl = Check.NotNullOrWhiteSpace(resourceUrl, nameof(resourceUrl));
            Type = Check.NotNullOrWhiteSpace(type, nameof(type));
            Comment = comment;
        }
    }
}
