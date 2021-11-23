using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Admin.Credentials
{
    public class GetCredentialsInput : PagedAndSortedResultRequestDto
    {
        public Guid? UserId { get; set; }
    }
}
