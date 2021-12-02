using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.Feeds
{
    public class UpdateFeedDto : CreateOrUpdateFeedDto, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }
    }
}
