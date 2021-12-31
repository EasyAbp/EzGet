using EasyAbp.EzGet.Feeds;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EzGet.Admin.Feeds
{
    public class FeedAdminAppService_Tests : EzGetApplicationTestBase
    {
        private readonly IFeedAdminAppService _feedAdminAppService;
        private readonly EzGetTestData _ezGetTestData;

        public FeedAdminAppService_Tests()
        {
            _feedAdminAppService = GetRequiredService<IFeedAdminAppService>();
            _ezGetTestData = GetRequiredService<EzGetTestData>();
        }

        [Fact]
        public async Task CreateAsync()
        {
            var input = new CreateFeedAdminDto
            {
                UserId = _ezGetTestData.User1Id,
                FeedName = "SayHello",
                Description = "Test feed",
                FeedType = FeedTypeEnum.Public,
                CredentialIds = new List<Guid>
                {
                    _ezGetTestData.User1CredentialId
                }
            };

            var feed = await _feedAdminAppService.CreateAsync(input);
            feed.UserId.ShouldBe(_ezGetTestData.User1Id);
            feed.FeedName.ShouldBe("SayHello");
            feed.Description.ShouldBe("Test feed");
            feed.FeedType.ShouldBe(FeedTypeEnum.Public);
            feed.CredentialIds.Count.ShouldBe(1);
            feed.CredentialIds.First().ShouldBe(_ezGetTestData.User1CredentialId);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var feed = await _feedAdminAppService.FindByNameAsync(_ezGetTestData.User1FeedName);

            var updateInput = new UpdateFeedAdminDto
            {
                ConcurrencyStamp = feed.ConcurrencyStamp,
                UserId = _ezGetTestData.User2Id,
                Description = "update test",
                FeedType = FeedTypeEnum.Private,
                CredentialIds = new List<Guid>
                {
                    _ezGetTestData.User2CredentialId
                }
            };

            var newFeed = await _feedAdminAppService.UpdateAsync(feed.Id, updateInput);
            newFeed.UserId.ShouldBe(_ezGetTestData.User2Id);
            newFeed.FeedName.ShouldBe(feed.FeedName);
            newFeed.Description.ShouldBe("update test");
            newFeed.FeedType.ShouldBe(FeedTypeEnum.Private);
            newFeed.CredentialIds.Count.ShouldBe(1);
            newFeed.CredentialIds.First().ShouldBe(_ezGetTestData.User2CredentialId);
        }
    }
}
