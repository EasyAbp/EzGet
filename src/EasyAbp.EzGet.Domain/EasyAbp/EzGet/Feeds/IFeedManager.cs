using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EzGet.Feeds
{
    public interface IFeedManager : IDomainService
    {
        Task<Feed> CreateAsync(
            Guid userId,
            [NotNull] string feedName,
            FeedTypeEnum feedType,
            string description = null);

        Task AddCredentialAsync([NotNull] Feed feed, Guid credentialId);

        Task SetUserIdAsync(Feed feed, Guid userId);

        Task<bool> CheckFeedPermissionAsync([NotNull] Feed feed, Guid? userId);
    }
}
