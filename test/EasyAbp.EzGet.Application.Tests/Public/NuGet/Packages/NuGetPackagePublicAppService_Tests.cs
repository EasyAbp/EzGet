using EasyAbp.EzGet.NuGet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;
using Xunit;
using EasyAbp.EzGet.NuGet.Packages;
using Shouldly;

namespace EasyAbp.EzGet.Public.NuGet.Packages
{
    public class NuGetPackagePublicAppService_Tests : EzGetApplicationTestBase
    {
        private INuGetPackagePublicAppService _nuGetPackagePublicAppService;
        private IBlobContainer<NuGetContainer> _blobContainer;
        private INuGetPackageManager _nuGetPackageManager;
        private INuGetPackageRepository _nuGetPackageRepository;

        public NuGetPackagePublicAppService_Tests()
        {
            _nuGetPackagePublicAppService = GetRequiredService<INuGetPackagePublicAppService>();
            _blobContainer = GetRequiredService<IBlobContainer<NuGetContainer>>();
            _nuGetPackageManager = GetRequiredService<INuGetPackageManager>();
            _nuGetPackageRepository = GetRequiredService<INuGetPackageRepository>();
        }

        [Fact]
        public async Task NuGetPackage_ShouldBe_Create_And_File_ShouldBe_Saved()
        {
            var fileName = "Gsx.Abp.Excel.Abstractions.0.4.3";
            var filePath = Path.Combine("Resources", fileName);

            try
            {
                using (var fs = File.OpenRead(filePath))
                {
                    var packageDto = await _nuGetPackagePublicAppService.CreateAsync(new CreateNuGetPackageInputWithStream
                    {
                        File = new RemoteStreamContent(fs, fileName)
                    });

                    packageDto.ShouldNotBeNull();
                    packageDto.PackageName.ShouldBe("Gsx.Abp.Excel.Abstractions");
                    packageDto.Authors.Length.ShouldBe(1);
                    packageDto.Authors[0].ShouldBe("Gsx");
                    packageDto.Description.ShouldBe("Package Description");
                    packageDto.NormalizedVersion.ShouldBe("0.4.3");
                    packageDto.OriginalVersion.ShouldBe("0.4.3");

                    packageDto.Dependencies.Count.ShouldBe(1);
                    var dependencie = packageDto.Dependencies.First();
                    dependencie.DependencyPackageName.ShouldBe("Volo.Abp.Json");
                    dependencie.VersionRange.ShouldBe("[4.4.0, )");
                    dependencie.TargetFramework.ShouldBe("netstandard2.0");

                    packageDto.TargetFrameworks.Count.ShouldBe(1);
                    var targetFramework = packageDto.TargetFrameworks.First();
                    targetFramework.Moniker.ShouldBe("netstandard2.0");

                    var package = await _nuGetPackageRepository.GetAsync(packageDto.Id);
                    (await _blobContainer.ExistsAsync(await _nuGetPackageManager.GetNupkgBlobNameAsync(package))).ShouldBeTrue();
                    (await _blobContainer.ExistsAsync(await _nuGetPackageManager.GetNuspecBlobNameAsync(package))).ShouldBeTrue();
                    (await _blobContainer.ExistsAsync(await _nuGetPackageManager.GetIconBlobNameAsync(package))).ShouldBeFalse();
                    (await _blobContainer.ExistsAsync(await _nuGetPackageManager.GetReadmeBlobNameAsync(package))).ShouldBeFalse();

                    (await _blobContainer.DeleteAsync(await _nuGetPackageManager.GetNupkgBlobNameAsync(package))).ShouldBeTrue();
                    (await _blobContainer.DeleteAsync(await _nuGetPackageManager.GetNuspecBlobNameAsync(package))).ShouldBeTrue();
                    (await _blobContainer.DeleteAsync(await _nuGetPackageManager.GetIconBlobNameAsync(package))).ShouldBeFalse();
                    (await _blobContainer.DeleteAsync(await _nuGetPackageManager.GetReadmeBlobNameAsync(package))).ShouldBeFalse();
                }
            }
            finally
            {
                var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Pacakges"));

                if(dir.Exists)
                    dir.Delete(true);
            }
        }
    }
}
