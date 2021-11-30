using EasyAbp.EzGet.Admin.Credentials;
using EasyAbp.EzGet.Credentials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Validation;

namespace EasyAbp.EzGet.Admin.Web.Pages.EzGet.Credentials
{
    public class EditModalModel : EzGetAdminPageModel
    {
        [BindProperty(SupportsGet = true)]
        public CredentialInfoViewModel CredentialInfo { get; set; }

        protected ICredentialAdminAppService CredentialAdminAppService { get; }

        public EditModalModel(ICredentialAdminAppService credentialAdminAppService)
        {
            CredentialAdminAppService = credentialAdminAppService;
        }

        public virtual async Task<IActionResult> OnGetAsync(Guid id)
        {
            var credential = await CredentialAdminAppService.GetAsync(id);
            CredentialInfo = ObjectMapper.Map<CredentialDto, CredentialInfoViewModel>(credential);

            foreach (var scope in credential.Scopes)
            {
                if(scope.AllowAction == ScopeAllowActionEnum.Read)
                    CredentialInfo.Read = true;

                if (scope.AllowAction == ScopeAllowActionEnum.Write)
                    CredentialInfo.Write = true;
            }

            return Page();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var input = new UpdateCredentialDto
            {
                Description = CredentialInfo.Description,
                Expires = CredentialInfo.Expires,
                Scopes = new List<ScopeAllowActionEnum>()
            };

            if (CredentialInfo.Read)
                input.Scopes.Add(ScopeAllowActionEnum.Read);

            if (CredentialInfo.Write)
                input.Scopes.Add(ScopeAllowActionEnum.Write);

            await CredentialAdminAppService.UpdateAsync(CredentialInfo.Id, input);
            return NoContent();
        }

        public class CredentialInfoViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [ReadOnlyInput]
            public Guid UserId { get; set; }

            [DynamicStringLength(typeof(CredentialConsts), nameof(CredentialConsts.MaxDescriptionLength))]
            public string Description { get; set; }

            public DateTime? Expires { get; set; }

            [ReadOnlyInput]
            public string GlobPattern { get; set; }

            public bool Read { get; set; }

            public bool Write { get; set; }
        }
    }
}
