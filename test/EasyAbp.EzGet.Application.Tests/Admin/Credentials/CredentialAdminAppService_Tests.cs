using EasyAbp.EzGet.Credentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Volo.Abp.Users;
using EasyAbp.EzGet.Users;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Admin.Credentials
{
    public class CredentialAdminAppService_Tests : EzGetApplicationTestBase
    {
        private readonly ICredentialAdminAppService _credentialAdminAppService;
        private readonly EzGetTestData _ezGetTestData;
        private readonly IEzGetUserLookupService _ezGetUserLookupService;

        public CredentialAdminAppService_Tests()
        {
            _credentialAdminAppService = GetRequiredService<ICredentialAdminAppService>();
            _ezGetTestData = GetRequiredService<EzGetTestData>();
            _ezGetUserLookupService = GetRequiredService<IEzGetUserLookupService>();
        }

        [Fact]
        public async Task CreateAsync()
        {
            var input = new CreateCredentialDto
            {
                UserId = _ezGetTestData.User1Id,
                Expiration = TimeSpan.FromDays(1),
                GlobPattern = "Gsx.Abp.*",
                Description = "Test Credential",
                Scopes = new List<ScopeAllowActionEnum>
                {
                    ScopeAllowActionEnum.Read,
                    ScopeAllowActionEnum.Write
                }
            };

            var credential = await _credentialAdminAppService.CreateAsync(input);
            credential.Expires.HasValue.ShouldBeTrue();
            credential.Expires.Value.Date.ShouldBe(DateTime.Now.Date.AddDays(1));
            credential.GlobPattern.ShouldBe("Gsx.Abp.*");
            credential.Description.ShouldBe("Test Credential");
            credential.Scopes.Count.ShouldBe(2);
            credential.Scopes.ToArray()[0].AllowAction.ShouldNotBe(credential.Scopes.ToArray()[1].AllowAction);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var credentialsList = await _credentialAdminAppService.GetListAsync(new GetCredentialsInput());
            var credential = credentialsList.Items.First();

            var updateInput = new UpdateCredentialDto
            {
                ConcurrencyStamp = credential.ConcurrencyStamp,
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
