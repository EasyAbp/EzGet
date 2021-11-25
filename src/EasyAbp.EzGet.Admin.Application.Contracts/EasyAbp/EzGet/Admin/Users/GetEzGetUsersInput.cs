using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Admin.Users
{
    public class GetEzGetUsersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public string userName { get; set; }
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }
    }
}
