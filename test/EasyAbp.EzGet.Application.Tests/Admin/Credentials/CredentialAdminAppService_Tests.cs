using EasyAbp.EzGet.Credentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace EasyAbp.EzGet.Admin.Credentials
{
    public class CredentialAdminAppService_Tests : EzGetApplicationTestBase
    {
        private readonly ICredentialAdminAppService _credentialAdminAppService;

        public CredentialAdminAppService_Tests()
        {
            _credentialAdminAppService = GetRequiredService<ICredentialAdminAppService>();
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var credentialsList = await _credentialAdminAppService.GetListAsync(new GetCredentialsInput());
            var credential = credentialsList.Items.First();

            var updateInput = new UpdateCredentialDto
            {
                Expires = credential.Expires.Value.AddDays(1),
                Description = "Test",
                Scopes = new List<ScopeAllowActionEnum>()
                {
                    ScopeAllowActionEnum.Read
                }
            };

            var newCredential = await _credentialAdminAppService.UpdateAsync(credential.Id, updateInput);
            newCredential.Expires.Value.ShouldBeGreaterThan(credential.Expires.Value);
            newCredential.Description.ShouldBe("Test");
            newCredential.Scopes.Count.ShouldBe(1);
            newCredential.Scopes.First().AllowAction.ShouldBe(ScopeAllowActionEnum.Read);
        }
    }
}
