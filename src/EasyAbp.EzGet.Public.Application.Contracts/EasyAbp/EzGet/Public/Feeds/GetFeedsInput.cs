using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Public.Feeds
{
    public class GetFeedsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
