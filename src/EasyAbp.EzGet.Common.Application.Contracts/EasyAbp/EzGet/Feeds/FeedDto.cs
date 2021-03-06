using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.Feeds
{
    public class FeedDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public Guid UserId { get; set; }
        public string FeedName { get; set; }
        public string Description { get; set; }
        public FeedTypeEnum FeedType { get; set; }
        public ICollection<Guid> CredentialIds { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
