using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Credentials
{
    public class CredentialScopeDto : EntityDto
    {
        public Guid CredentialId { get; set; }
        public ScopeAllowActionEnum AllowAction { get; set; }
    }
}
