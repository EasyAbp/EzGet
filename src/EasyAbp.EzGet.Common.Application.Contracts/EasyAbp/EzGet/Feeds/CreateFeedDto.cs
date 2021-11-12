using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Feeds
{
    public class CreateFeedDto : CreateOrUpdateFeedDto
    {
        public string FeedName { get; set; }
    }
}
