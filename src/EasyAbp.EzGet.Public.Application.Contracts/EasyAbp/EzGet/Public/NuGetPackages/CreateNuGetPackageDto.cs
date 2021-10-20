using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Content;

namespace EasyAbp.EzGet.Public.NuGetPackages
{
    public class CreateNuGetPackageDto
    {
        public IRemoteStreamContent File { get; set; }
    }
}
