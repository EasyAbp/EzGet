using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace EasyAbp.EzGet.Users
{
    public class EzGetUserLookupService : UserLookupService<EzGetUser, IEzGetUserRepository>, IEzGetUserLookupService
    {
        public EzGetUserLookupService(
            IEzGetUserRepository userRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(userRepository, unitOfWorkManager)
        {
        }

        protected override EzGetUser CreateUser(IUserData externalUser)
        {
            return new EzGetUser(externalUser);
        }
    }
}
