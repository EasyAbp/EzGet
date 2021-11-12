using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Feeds
{
    public class FeedDto : FullAuditedEntityDto<Guid>
    {
        public string FeedName { get; }
        public string Description { get; set; }
        public FeedTypeEnum FeedType { get; }
        public ICollection<Guid> CredentialIds { get; }
    }
}
