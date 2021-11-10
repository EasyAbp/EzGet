using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Caching;

namespace EasyAbp.EzGet.Credentials
{
    [CacheName("Credential")]
    public class CredentialCacheItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Value { get; set; }
        public DateTime? Expires { get; set; }
        public string GlobPattern { get; set; }
        public List<ScopeAllowActionEnum> Scopes { get; set; }
    }
}
