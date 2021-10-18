using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.Credentials
{
    public class Scope : Entity<Guid>
    {
        public Guid CredentialId { get; }
        public string GlobPattern { get; }
        public ScopeAllowActionEnum AllowAction { get; }

        public Scope([NotNull] Credential credential, string globPattern, ScopeAllowActionEnum allowAction)
        {
            Check.NotNull(credential, nameof(credential));
            CredentialId = credential.Id;
            GlobPattern = globPattern;
            AllowAction = allowAction;
        }
    }
}
