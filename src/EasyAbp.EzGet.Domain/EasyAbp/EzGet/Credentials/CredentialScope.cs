using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.Credentials
{
    public class CredentialScope : Entity
    {
        public Guid CredentialId { get; }
        public ScopeAllowActionEnum AllowAction { get; }

        private CredentialScope()
        {
        }

        public CredentialScope([NotNull] Credential credential, ScopeAllowActionEnum allowAction)
        {
            Check.NotNull(credential, nameof(credential));
            CredentialId = credential.Id;
            AllowAction = allowAction;
        }

        public override object[] GetKeys()
        {
            return new object[] { CredentialId, AllowAction };
        }
    }
}
