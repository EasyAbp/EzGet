using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public class NuGetPackageDto : FullAuditedEntityDto<Guid>
    {
        public string PackageName { get; set; }
        public string[] Authors { get; set; }
        public string Description { get; set; }
        public long Downloads { get; set; }
        public bool HasReadme { get; set; }
        public bool HasEmbeddedIcon { get; set; }
        public bool IsPrerelease { get; set; }
        public string ReleaseNotes { get; set; }
        public string Language { get; set; }
        public bool Listed { get; set; }
        public string MinClientVersion { get; set; }
        public DateTime Published { get; set; }
        public bool RequireLicenseAcceptance { get; set; }
        public SemVerLevelEnum SemVerLevel { get; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public string IconUrl { get; set; }
        public string LicenseUrl { get; set; }
        public string ProjectUrl { get; set; }
        public string RepositoryUrl { get; set; }
        public string RepositoryType { get; set; }
        public string[] Tags { get; set; }
        public string NormalizedVersion { get; set; }
        public string OriginalVersion { get; set; }

        public ICollection<PackageDependencyDto> Dependencies { get; set; }
        public ICollection<PackageTypeDto> PackageTypes { get; set; }
        public ICollection<TargetFrameworkDto> TargetFrameworks { get; set; }
    }
}
