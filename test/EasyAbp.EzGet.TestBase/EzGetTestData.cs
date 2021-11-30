using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EzGet
{
    public class EzGetTestData : ISingletonDependency
    {
        public Guid User1CredentialId { get; } = Guid.NewGuid();
        public Guid User2CredentialId { get; } = Guid.NewGuid();

        public string User1FeedName { get; } = "feed1";
        public string User2FeedName { get; } = "feed2";

        public Guid User1Id { get; } = Guid.NewGuid();
        public Guid User2Id { get; } = Guid.NewGuid();
    }
}
