using EasyAbp.EzGet.Admin.NuGet.Packages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace EasyAbp.EzGet.Admin.Web.Pages.EzGet.NuGet.Packages
{
    public class CreateModalModel : EzGetAdminPageModel
    {
        [BindProperty]
        public NuGetPackageViewModal NuGetPackageInfo { get; set; }

        protected INuGetPackageAdminAppService NuGetPackageAdminAppService { get; }

        public CreateModalModel(INuGetPackageAdminAppService nuGetPackageAdminAppService)
        {
            NuGetPackageAdminAppService = nuGetPackageAdminAppService;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            using (var ms = new MemoryStream())
            {
                await NuGetPackageInfo.File.CopyToAsync(ms);

                var createInput = new CreateNuGetPackageInputWithStream
                {
                    FeedId = NuGetPackageInfo.FeedId,
                    File = new RemoteStreamContent(ms)
                };

                await NuGetPackageAdminAppService.CreateAsync(createInput);
                return NoContent();
            }
        }

        public class NuGetPackageViewModal
        {
            public Guid FeedId { get; set; }
            public IFormFile File { get; set; }
        }
    }
}
