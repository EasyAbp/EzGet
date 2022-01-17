using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Feeds
{
    public abstract class CreateOrUpdateFeedDto
    {
        public string Description { get; set; }
        public FeedTypeEnum FeedType { get; set; }
        public ICollection<Guid> CredentialIds { get; set; } = new List<Guid>();
    }
}
