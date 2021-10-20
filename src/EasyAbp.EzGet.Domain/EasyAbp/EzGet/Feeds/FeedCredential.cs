using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.Feeds
{
    public class FeedCredential : Entity
    {
        public Guid FeedId { get; }
        public Guid CredentialId { get; }

        public override object[] GetKeys()
        {
            return new object[] { FeedId, CredentialId };
        }
    }
}
