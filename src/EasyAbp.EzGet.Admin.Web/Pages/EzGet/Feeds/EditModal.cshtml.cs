using EasyAbp.EzGet.Admin.Credentials;
using EasyAbp.EzGet.Admin.Feeds;
using EasyAbp.EzGet.Credentials;
using EasyAbp.EzGet.Feeds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public List<FeedCredentialViewModel> FeedCredentials { get; set; }

        public List<SelectListItem> FeedTypeList { get; set; }

        protected IFeedAdminAppService FeedAdminAppService { get; }
        protected ICredentialAdminAppService CredentialAdminAppService { get; }

        public EditModalModel(
            IFeedAdminAppService feedAdminAppService,
            ICredentialAdminAppService credentialAdminAppService)
        {
            FeedAdminAppService = feedAdminAppService;
            CredentialAdminAppService = credentialAdminAppService;
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

            var credentials = await CredentialAdminAppService.GetListByFeedIdAsync(id);

            FeedCredentials = credentials.Select(p =>
            {
                var feedCredential = new FeedCredentialViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Expires = p.Expires
                };

                foreach (var scope in p.Scopes)
                {
                    if (scope.AllowAction == ScopeAllowActionEnum.Read)
                        feedCredential.Read = true;

                    if (scope.AllowAction == ScopeAllowActionEnum.Write)
                        feedCredential.Write = true;
                }

                return feedCredential;

            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

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

        public class FeedCredentialViewModel
        {
            public Guid Id { get; set; }
            public string Description { get; set; }
            public DateTime? Expires { get; set; }
            public bool Read { get; set; }
            public bool Write { get; set; }
        }
    }
}
