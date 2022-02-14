using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EasyAbp.EzGet.Public.PackageRegistrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EasyAbp.EzGet.Public.Web.Pages.Public.EzGet.Feeds;

public class RemoveOwnerModal : EzGetPublicPageModel
{
    [BindProperty]
    public RemoveOwnerModalViewModel RemoveOwnerInfo { get; set; }
    
    [FromQuery]
    public Guid Id { get; set; }
    
    protected IPackageRegistrationPublicAppService PackageRegistrationPublicAppService { get; }

    public RemoveOwnerModal(IPackageRegistrationPublicAppService packageRegistrationPublicAppService)
    {
        PackageRegistrationPublicAppService = packageRegistrationPublicAppService;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await PackageRegistrationPublicAppService.RemoveOwnerAsync(Id, RemoveOwnerInfo.UserId);
        return NoContent();
    }

    public class RemoveOwnerModalViewModel
    {
        [Required]
        public Guid UserId { get; set; }
    }
}