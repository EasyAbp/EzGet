using EasyAbp.EzGet.Credentials;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EzGet.DataSeed
{
    public class EzGetUnifiedDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private IIdentityUserRepository _identityUserRepository;
        private ICredentialRepository _credentialRepository;
        private IGuidGenerator _guidGenerator;
        private ICurrentTenant _currentTenant;

        public EzGetUnifiedDataSeedContributor(
            IIdentityUserRepository identityUserRepository,
            ICredentialRepository credentialRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant)
        {
            _identityUserRepository = identityUserRepository;
            _credentialRepository = credentialRepository;
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context?.TenantId))
            {
                if (await _credentialRepository.FindByValueAsync("123") != null)
                {
                    return;
                }

                var user = await _identityUserRepository.FindByNormalizedUserNameAsync("admin");
                var credential = new Credential(_guidGenerator.Create(), user.Id, "123", null, null);
                await _credentialRepository.InsertAsync(credential);
            }
        }
    }
}
