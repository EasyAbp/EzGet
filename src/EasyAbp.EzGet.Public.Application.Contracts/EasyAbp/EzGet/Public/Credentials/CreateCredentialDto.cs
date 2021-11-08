using EasyAbp.EzGet.Credentials;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Public.Credentials
{
    public class CreateCredentialDto : CreateOrUpdateCredentialDto
    {
        public TimeSpan? Expiration { get; set; }
        public string GlobPattern { get; set; }
        public List<ScopeAllowActionEnum> Scopes { get; set; }
    }
}
