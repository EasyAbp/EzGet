using EasyAbp.EzGet.Feeds;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Admin.Feeds
{
    public class UpdateFeedAdminDto : UpdateFeedDto
    {
        public Guid UserId { get; set; }
    }
}
