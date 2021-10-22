using NuGet.Packaging;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public class NuGetPackageManager_Tests : EzGetDomainTestBase
    {
        private readonly INuGetPackageManager _nuGetPackageManager;

        public NuGetPackageManager_Tests()
        {
            _nuGetPackageManager = GetRequiredService<INuGetPackageManager>();
        }

        [Fact]
        public async Task Package_ShouldBe_Created()
        {
            var filePath = Path.Combine("Resources", "Gsx.Abp.Excel.Abstractions.0.4.3");
            NuGetPackage package = null;

            using (var fs = File.OpenRead(filePath))
            {
                using (var packageReader = new PackageArchiveReader(fs, leaveStreamOpen: true))
                {
                    package = await _nuGetPackageManager.CreateAsync(packageReader);
                }
            }

            package.ShouldNotBeNull();
            package.PackageName.ShouldBe("Gsx.Abp.Excel.Abstractions");
            package.Authors.Length.ShouldBe(1);
            package.Authors[0].ShouldBe("Gsx");
            package.Description.ShouldBe("Package Description");
            package.NormalizedVersion.ShouldBe("0.4.3");
            package.OriginalVersion.ShouldBe("0.4.3");

            package.Dependencies.Count.ShouldBe(1);
            var dependencie = package.Dependencies.First();
            dependencie.DependencyPackageName.ShouldBe("Volo.Abp.Json");
            dependencie.VersionRange.ShouldBe("[4.4.0, )");
            dependencie.TargetFramework.ShouldBe("netstandard2.0");

            package.TargetFrameworks.Count.ShouldBe(1);
            var targetFramework = package.TargetFrameworks.First();
            targetFramework.Moniker.ShouldBe("netstandard2.0");
        }
    }
}
