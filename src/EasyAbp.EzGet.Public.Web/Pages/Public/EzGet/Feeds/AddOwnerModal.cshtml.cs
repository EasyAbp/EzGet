using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EasyAbp.EzGet.PackageRegistrations;
using EasyAbp.EzGet.Public.PackageRegistrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.EzGet.Public.Web.Pages.Public.EzGet.Feeds;

public class AddOwnerModal : EzGetPublicPageModel
{
    [BindProperty]
    public AddOwnerModalViewModel AddOwnerModalInfo { get; set; }
    
    public List<SelectListItem> OwnerTypeList { get; set; }
    
    [FromQuery]
    public Guid Id { get; set; }
    
    protected IPackageRegistrationPublicAppService PackageRegistrationPublicAppService { get; }

    public AddOwnerModal(IPackageRegistrationPublicAppService packageRegistrationPublicAppService)
    {
        PackageRegistrationPublicAppService = packageRegistrationPublicAppService;
    }
    
    public void OnGet()
    {
        OwnerTypeList = new List<SelectListItem>
        {
            new() { Value = ((int)PackageRegistrationOwnerTypeEnum.Developer).ToString(), Text = L["Developer"]},
            new() { Value = ((int)PackageRegistrationOwnerTypeEnum.Maintainer).ToString(), Text = L["Maintainer"]}
        };
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await PackageRegistrationPublicAppService.AddOwnerAsync(Id, new AddOwnerDto
        {
            OwnerType = AddOwnerModalInfo.OwnerType,
            UserId = AddOwnerModalInfo.UserId
        });
        
        return NoContent();
    }

    public class AddOwnerModalViewModel
    {
        [Required]
        public Guid UserId { get; set; }
        
        [SelectItems(nameof(OwnerTypeList))]
        public PackageRegistrationOwnerTypeEnum OwnerType { get; set; }
    }
}