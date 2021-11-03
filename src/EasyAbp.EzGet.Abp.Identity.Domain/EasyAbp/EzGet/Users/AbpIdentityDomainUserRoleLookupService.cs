using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace EasyAbp.EzGet.Users
{
    public class AbpIdentityDomainUserRoleLookupService : IUserRoleLookupService, ITransientDependency
    {
        protected IUserRoleFinder UserRoleFinder { get; }

        public AbpIdentityDomainUserRoleLookupService(IUserRoleFinder userRoleFinder)
        {
            UserRoleFinder = userRoleFinder;
        }

        public async Task<IEnumerable<string>> FindRolesAsync(Guid id)
        {
            return await UserRoleFinder.GetRolesAsync(id);
        }
    }
}
