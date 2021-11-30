using EasyAbp.EzGet.Credentials;
using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.Users;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace EasyAbp.EzGet
{
    public class EzGetDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly ICredentialRepository _credentialRepository;
        private readonly IEzGetUserRepository _ezGetUserRepository;
        private readonly IFeedManager _feedManager;
        private readonly IFeedRepository _feedRepository;
        private readonly EzGetTestData _ezGetTestData;

        public EzGetDataSeedContributor(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            ICredentialRepository credentialRepository,
            IEzGetUserRepository ezGetUserRepository,
            IFeedManager feedManager,
            IFeedRepository feedRepository,
            EzGetTestData ezGetTestData)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _credentialRepository = credentialRepository;
            _ezGetUserRepository = ezGetUserRepository;
            _ezGetTestData = ezGetTestData;
            _feedManager = feedManager;
            _feedRepository = feedRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            /* Instead of returning the Task.CompletedTask, you can insert your test data
             * at this point!
             */

            using (_currentTenant.Change(context?.TenantId))
            {
                await SeedEzGetUsersAsync();
                await SeedFeedsAsync();
                await SeedCredentialsAsnyc();
            }
        }

        private async Task SeedFeedsAsync()
        {
            var feed1 = await _feedManager.CreateAsync(
                _ezGetTestData.User1Id,
                _ezGetTestData.User1FeedName,
                FeedTypeEnum.Public,
                "Test feed");

            await _feedManager.AddCredentialAsync(feed1, _ezGetTestData.User1CredentialId);

            var feed2 = await _feedManager.CreateAsync(
                _ezGetTestData.User2Id,
                _ezGetTestData.User2FeedName,
                FeedTypeEnum.Public,
                "Test feed");

            await _feedManager.AddCredentialAsync(feed2, _ezGetTestData.User2CredentialId);

            await _feedRepository.InsertManyAsync(new Feed[] { feed1, feed2 });
        }

        private async Task SeedCredentialsAsnyc()
        {
            var credential1 = new Credential(
                _ezGetTestData.User1CredentialId,
                _ezGetTestData.User1Id,
                Guid.NewGuid().ToString(),
                TimeSpan.FromDays(1),
                null,
                null);

            var credential2 = new Credential(
                _ezGetTestData.User2CredentialId,
                _ezGetTestData.User2Id,
                Guid.NewGuid().ToString(),
                TimeSpan.FromDays(1),
                null,
                null);

            await _credentialRepository.InsertManyAsync(new Credential[] { credential1, credential2 });
        }

        private async Task SeedEzGetUsersAsync()
        {
            var ezGetUser1 = new EzGetUser(new UserData(_ezGetTestData.User1Id, "user1", "user1@abp.io"));
            await _ezGetUserRepository.InsertAsync(ezGetUser1);

            var ezGetUser2 = new EzGetUser(new UserData(_ezGetTestData.User2Id, "user2", "user2@abp.io"));
            await _ezGetUserRepository.InsertAsync(ezGetUser2);
        }
    }
}
