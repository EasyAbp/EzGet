using EasyAbp.EzGet.Feeds;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Admin.Feeds
{
    public class CreateFeedAdminDto : CreateFeedDto
    {
        public Guid UserId { get; set; }
    }
}
