using EasyAbp.EzGet.Credentials;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EzGet.Public.Credentials
{
    public class CredentialScopeDto : EntityDto
    {
        public Guid CredentialId { get; set; }
        public ScopeAllowActionEnum AllowAction { get; set; }
    }
}
