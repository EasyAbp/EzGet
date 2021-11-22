using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EzGet.Credentials
{
    public class CredentialDto : FullAuditedEntityDto<Guid>, IMultiTenant
    {
        public Guid UserId { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public DateTime? Expires { get; set; }
        public string GlobPattern { get; set; }
        public Guid? TenantId { get; set; }
        public virtual ICollection<CredentialScopeDto> Scopes { get; set; }
    }
}
