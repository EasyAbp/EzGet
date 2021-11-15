using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Admin.Feeds
{
    public class GetFeedsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
