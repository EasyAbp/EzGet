using EasyAbp.EzGet.Credentials;
using EasyAbp.EzGet.Users;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EzGet.Feeds
{
    public class FeedManager : DomainService, IFeedManager
    {
        protected ICredentialRepository CredentialRepository { get; }
        protected IEzGetUserLookupService EzGetUserLookupService { get; }

        public FeedManager(
            ICredentialRepository credentialRepository,
            IEzGetUserLookupService ezGetUserLookupService)
        {
            CredentialRepository = credentialRepository;
            EzGetUserLookupService = ezGetUserLookupService;
        }

        public virtual async Task<Feed> CreateAsync(
            Guid userId,
            [NotNull] string feedName,
            FeedTypeEnum feedType,
            string description = null)
        {
            Check.NotNullOrWhiteSpace(feedName, nameof(feedName));
            await CheckUserAsync(userId);
            return new Feed(GuidGenerator.Create(), userId, feedName, feedType, description);
        }

        public virtual async Task AddCredentialAsync([NotNull] Feed feed, Guid credentialId)
        {
            Check.NotNull(feed, nameof(feed));

            var credential = await CredentialRepository.GetAsync(credentialId);

            if (null == credential)
            {
                throw new EntityNotFoundException();
            }

            if (credential.UserId != feed.UserId)
            {
                throw new BusinessException(EzGetErrorCodes.FeedCannotAddOtherUserCredential);
            }

            if (feed.FeedCredentials.Any(p => p.CredentialId == credentialId))
            {
                return;
            }

            feed.AddCredentialId(credentialId);
        }

        public virtual async Task SetUserIdAsync(Feed feed, Guid userId)
        {
            await CheckUserAsync(userId);
            feed.SetUserId(userId);
        }

        private async Task<EzGetUser> CheckUserAsync(Guid userId)
        {
            var user = await EzGetUserLookupService.FindByIdAsync(userId);

            if (null == user)
            {
                throw new BusinessException(EzGetErrorCodes.UserNotFound);
            }

            return user;
        }
    }
}
