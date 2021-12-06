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
using Volo.Abp.Validation;

namespace EasyAbp.EzGet.Admin.Web.Pages.EzGet.Feeds
{
    public class CreateModalModel : EzGetAdminPageModel
    {
        [BindProperty]
        public FeedInfoViewModel FeedInfo { get; set; }

        public List<SelectListItem> FeedTypeList { get; set; }

        protected IFeedAdminAppService FeedAdminAppService { get; }

        public CreateModalModel(IFeedAdminAppService feedAdminAppService)
        {
            FeedAdminAppService = feedAdminAppService;
        }

        public void OnGet()
        {
            FeedTypeList = new List<SelectListItem>
            {
                new SelectListItem { Value = ((int)FeedTypeEnum.Private).ToString(), Text = L["Private"]},
                new SelectListItem { Value = ((int)FeedTypeEnum.Public).ToString(), Text = L["Public"]}
            };
        }

        public virtual async Task<IActionResult> PostAsync()
        {
            ValidateModel();

            var input = ObjectMapper.Map<FeedInfoViewModel, CreateFeedAdminDto>(FeedInfo);
            await FeedAdminAppService.CreateAsync(input);
            return NoContent();
        }

        public class FeedInfoViewModel
        {
            [Required]
            public Guid UserId { get; set; }

            [Required]
            [DynamicStringLength(typeof(FeedConsts), nameof(FeedConsts.MaxFeedNameLenght))]
            public string FeedName { get; set; }

            [DynamicStringLength(typeof(FeedConsts), nameof(FeedConsts.MaxDescriptionLength))]
            public string Description { get; set; }

            [SelectItems(nameof(FeedTypeList))]
            public FeedTypeEnum FeedType { get; set; }

            public List<Guid> CredentialIds { get; set; } = new List<Guid>();
        }
    }
}
