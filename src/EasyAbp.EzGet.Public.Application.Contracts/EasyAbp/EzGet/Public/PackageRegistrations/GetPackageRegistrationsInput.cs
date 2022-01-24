using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Public.PackageRegistrations
{
    public class GetPackageRegistrationsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public Guid? FeedId { get; set; }
    }
}
