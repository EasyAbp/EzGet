using EasyAbp.EzGet.Admin.Credentials;
using EasyAbp.EzGet.Credentials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Validation;

namespace EasyAbp.EzGet.Admin.Web.Pages.EzGet.Credentials
{
    public class CreateModalModel : EzGetAdminPageModel
    {
        [BindProperty]
        public CredentialInfoViewModel CredentialInfo { get; set; }

        public List<SelectListItem> ExpirationTypeList { get; set; }

        protected ICredentialAdminAppService CredentialAdminAppService { get; }

        public CreateModalModel(ICredentialAdminAppService credentialAdminAppService)
        {
            CredentialAdminAppService = credentialAdminAppService;
        }

        public void OnGet()
        {
            ExpirationTypeList = new List<SelectListItem>
            {
                new SelectListItem { Value = ((int)ExpirationTypeEnum.Forever).ToString(), Text = L["Forever"]},
                new SelectListItem { Value = ((int)ExpirationTypeEnum.OneDay).ToString(), Text = L["OneDay"]},
                new SelectListItem { Value = ((int)ExpirationTypeEnum.OneWeek).ToString(), Text = L["OneWeek"]},
                new SelectListItem { Value = ((int)ExpirationTypeEnum.OneMonth).ToString(), Text = L["OneMonth"]},
                new SelectListItem { Value = ((int)ExpirationTypeEnum.ThreeMonth).ToString(), Text = L["ThreeMonth"]},
                new SelectListItem { Value = ((int)ExpirationTypeEnum.SixMonth).ToString(), Text = L["SixMonth"]},
                new SelectListItem { Value = ((int)ExpirationTypeEnum.OneYear).ToString(), Text = L["SixMonth"]},
            };
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var input = ObjectMapper.Map<CredentialInfoViewModel, CreateCredentialDto>(CredentialInfo);

            input.Expiration = GetTimeSpan(CredentialInfo.ExpirationType);
            input.Scopes ??= new List<ScopeAllowActionEnum>();

            if (CredentialInfo.Read)
                input.Scopes.Add(ScopeAllowActionEnum.Read);

            if(CredentialInfo.Write)
                input.Scopes.Add(ScopeAllowActionEnum.Write);

            var credential = await CredentialAdminAppService.CreateAsync(input);

            return Content(credential.Value);
        }

        private TimeSpan? GetTimeSpan(ExpirationTypeEnum expirationType) 
        {
            switch (expirationType)
            {
                default:
                case ExpirationTypeEnum.Forever:
                    return null;

                case ExpirationTypeEnum.OneDay:
                    return TimeSpan.FromDays(1);

                case ExpirationTypeEnum.OneWeek:
                    return TimeSpan.FromDays(7);

                case ExpirationTypeEnum.OneMonth:
                    return TimeSpan.FromDays(31);

                case ExpirationTypeEnum.ThreeMonth:
                    return TimeSpan.FromDays(31 * 3);

                case ExpirationTypeEnum.SixMonth:
                    return TimeSpan.FromDays(31 * 6);

                case ExpirationTypeEnum.OneYear:
                    return TimeSpan.FromDays(365);
            }
        }

        public class CredentialInfoViewModel
        {
            [Required]
            public Guid UserId { get; set; }

            [DynamicStringLength(typeof(CredentialConsts), nameof(CredentialConsts.MaxDescriptionLength))]
            public string Description { get; set; }

            [SelectItems(nameof(ExpirationTypeList))]
            public ExpirationTypeEnum ExpirationType { get; set; }

            [DynamicStringLength(typeof(CredentialConsts), nameof(CredentialConsts.MaxGlobPatternLength))]
            public string GlobPattern { get; set; }

            public bool Read { get; set; }

            public bool Write { get; set; }
        }

        public enum ExpirationTypeEnum
        {
            Forever,
            OneDay,
            OneWeek,
            OneMonth,
            ThreeMonth,
            SixMonth,
            OneYear
        }
    }
}
