using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Users
{
    //It can use Volo.Abp.Identity.Application, or others way.
    //For now we use Volo.Abp.Identity.Domain for find user roles.
    public interface IUserRoleLookupService
    {
        Task<IEnumerable<string>> FindRolesAsync(Guid id);
    }
}
