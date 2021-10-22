using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Content;

namespace EasyAbp.EzGet.Public.NuGet.Packages
{
    public class CreateNuGetPackageInputWithStream
    {
        public IRemoteStreamContent File { get; set; }
    }
}
