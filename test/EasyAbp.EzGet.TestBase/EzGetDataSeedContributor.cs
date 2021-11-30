using EasyAbp.EzGet.Credentials;
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
        protected readonly ICurrentUser _currentUser;

        public EzGetDataSeedContributor(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            ICredentialRepository credentialRepository,
            ICurrentUser currentUser)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _credentialRepository = credentialRepository;
            _currentUser = currentUser;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            /* Instead of returning the Task.CompletedTask, you can insert your test data
             * at this point!
             */

            using (_currentTenant.Change(context?.TenantId))
            {
                await SeedCredentialAsnyc();
            }
        }

        private async Task SeedCredentialAsnyc()
        {
            var credential = new Credential(
                _guidGenerator.Create(),
                _currentUser.Id.Value,
                Guid.NewGuid().ToString(),
                TimeSpan.FromDays(1),
                null,
                null);

            await _credentialRepository.InsertAsync(credential);
        }
    }
}
