using EasyAbp.EzGet.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users.EntityFrameworkCore;

namespace EasyAbp.EzGet.Users
{
    public class EfCoreEzGetUserRepository : EfCoreUserRepositoryBase<IEzGetDbContext, EzGetUser>, IEzGetUserRepository
    {
        public EfCoreEzGetUserRepository(IDbContextProvider<IEzGetDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
