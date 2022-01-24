using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.Public.Feeds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Validation;

namespace EasyAbp.EzGet.Public.Web.Pages.Public.EzGet.Feeds
{
    public class CreateModel : EzGetPublicPageModel
    {
        [BindProperty]
        public FeedInfoViewModel FeedInfo { get; set; }

        public List<SelectListItem> FeedTypeList { get; set; }

        protected IFeedPublicAppService FeedPublicAppService { get; }

        public CreateModel(IFeedPublicAppService feedPublicAppService)
        {
            FeedPublicAppService = feedPublicAppService;
        }

        public virtual void OnGet()
        {
            FeedTypeList = new List<SelectListItem>
            {
                new SelectListItem { Value = ((int)FeedTypeEnum.Public).ToString(), Text = L["Public"]},
                new SelectListItem { Value = ((int)FeedTypeEnum.Private).ToString(), Text = L["Private"]}
            };
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var feed = await FeedPublicAppService.CreateAsync(new CreateFeedPublicDto
            {
                FeedName = FeedInfo.FeedName,
                Description = FeedInfo.Description,
                FeedType = FeedInfo.FeedType
            });

            return RedirectSafely($"/Public/EzGet/Feeds/{feed.FeedName}/Packages");
        }

        public class FeedInfoViewModel
        {
            [Required]
            [DynamicStringLength(typeof(FeedConsts), nameof(FeedConsts.MaxFeedNameLenght))]
            public string FeedName { get; set; }

            [DynamicStringLength(typeof(FeedConsts), nameof(FeedConsts.MaxDescriptionLength))]
            public string Description { get; set; }

            [SelectItems(nameof(FeedTypeList))]
            public FeedTypeEnum FeedType { get; set; }
        }
    }
}
