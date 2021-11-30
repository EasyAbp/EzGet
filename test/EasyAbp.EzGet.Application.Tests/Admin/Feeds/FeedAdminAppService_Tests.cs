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

        public FeedAdminAppService_Tests()
        {
            _feedAdminAppService = GetRequiredService<IFeedAdminAppService>();
        }

        [Fact]
        public async Task CreateAsync()
        {
            var input = new CreateFeedAdminDto
            {

            };
        }
    }
}
