using System;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.Admin.Credentials
{
    public class UpdateCredentialDto : CreateOrUpdateCredentialDto, IHasConcurrencyStamp
    {
        public DateTime? Expires { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
