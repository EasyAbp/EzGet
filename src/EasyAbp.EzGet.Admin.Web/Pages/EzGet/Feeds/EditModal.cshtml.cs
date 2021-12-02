using EasyAbp.EzGet.Admin.Feeds;
using EasyAbp.EzGet.Feeds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Validation;

namespace EasyAbp.EzGet.Admin.Web.Pages.EzGet.Feeds
{
    public class EditModalModel : EzGetAdminPageModel
    {
        [BindProperty(SupportsGet = true)]
        public FeedInfoViewModel FeedInfo { get; set; }

        public List<SelectListItem> FeedTypeList { get; set; }

        protected IFeedAdminAppService FeedAdminAppService { get; }

        public EditModalModel(IFeedAdminAppService feedAdminAppService)
        {
            FeedAdminAppService = feedAdminAppService;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            FeedTypeList = new List<SelectListItem>
            {
                new SelectListItem { Value = ((int)FeedTypeEnum.Private).ToString(), Text = L["Private"]},
                new SelectListItem { Value = ((int)FeedTypeEnum.Public).ToString(), Text = L["Public"]}
            };

            var feed = await FeedAdminAppService.GetAsync(id);
            FeedInfo = ObjectMapper.Map<FeedDto, FeedInfoViewModel>(feed);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var input = ObjectMapper.Map<FeedInfoViewModel, UpdateFeedAdminDto>(FeedInfo);
            await FeedAdminAppService.UpdateAsync(FeedInfo.Id, input);
            return NoContent();
        }

        public class FeedInfoViewModel : IHasConcurrencyStamp
        {
            [Required]
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [HiddenInput]
            public string ConcurrencyStamp { get; set; }

            [Required]
            public Guid UserId { get; set; }

            [ReadOnlyInput]
            public string FeedName { get; set; }

            [DynamicStringLength(typeof(FeedConsts), nameof(FeedConsts.MaxDescriptionLength))]
            public string Description { get; set; }

            [SelectItems(nameof(FeedTypeList))]
            public FeedTypeEnum FeedType { get; set; }

            public List<Guid> CredentialIds { get; set; } = new List<Guid>();
        }
    }
}
