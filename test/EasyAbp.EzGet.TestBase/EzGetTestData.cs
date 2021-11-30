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

        public Guid User1FeedId { get; } = Guid.NewGuid();
        public Guid User2FeedId { get; } = Guid.NewGuid();

        public Guid User1Id { get; } = Guid.NewGuid();
        public Guid User2Id { get; } = Guid.NewGuid();
    }
}
