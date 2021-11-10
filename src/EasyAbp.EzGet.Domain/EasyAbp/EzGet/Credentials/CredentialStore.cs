using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EzGet.Credentials
{
    public class CredentialStore : ICredentialStore, ITransientDependency
    {
        protected ICredentialRepository CredentialRepository { get; }
        protected IDistributedCache<CredentialCacheItem> Cache { get; }

        public CredentialStore(ICredentialRepository credentialRepository)
        {
            CredentialRepository = credentialRepository;
        }

        public async Task<CredentialCacheItem> GetAsync([NotNull] string value)
        {
            Check.NotNullOrWhiteSpace(value, nameof(value));

            return await Cache.GetOrAddAsync(value, async () =>
            {
                var credential = await CredentialRepository.FindByValueAsync(value);

                if (null == credential)
                {
                    return null;
                }

                return new CredentialCacheItem
                {
                    Id = credential.Id,
                    UserId = credential.UserId,
                    Value = value,
                    Expires = credential.Expires,
                    GlobPattern = credential.GlobPattern,
                    Scopes = credential.Scopes.Select(p => p.AllowAction).ToList()
                };
            },
            () => new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(30) });
        }
    }
}
