using EasyAbp.EzGet.Admin.NuGet.Packages;
using EasyAbp.EzGet.NuGet.Packages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.Admin.Web.Pages.EzGet.NuGet.Packages
{
    public class EditModalModel : EzGetAdminPageModel
    {
        [BindProperty(SupportsGet = true)]
        public NuGetPackageViewModal NuGetPackageInfo { get; set; }
        protected INuGetPackageAdminAppService NuGetPackageAdminAppService { get; }

        public EditModalModel(INuGetPackageAdminAppService nuGetPackageAdminAppService)
        {
            NuGetPackageAdminAppService = nuGetPackageAdminAppService;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var package = await NuGetPackageAdminAppService.GetAsync(id);
            NuGetPackageInfo = ObjectMapper.Map<NuGetPackageDto, NuGetPackageViewModal>(package);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await NuGetPackageAdminAppService.UpdateAsync(
                NuGetPackageInfo.Id,
                ObjectMapper.Map<NuGetPackageViewModal, UpdateNuGetPackageDto>(NuGetPackageInfo));

            return NoContent();
        }

        public class NuGetPackageViewModal : IHasConcurrencyStamp
        {
            [Required]
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [HiddenInput]
            public string ConcurrencyStamp { get; set; }

            [ReadOnlyInput]
            public string PackageName { get; set; }

            [ReadOnlyInput]
            public string NormalizedVersion { get; set; }

            public bool Listed { get; set; }
        }
    }
}
