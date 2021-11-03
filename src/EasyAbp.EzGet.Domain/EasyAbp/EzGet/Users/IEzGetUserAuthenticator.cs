using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Users
{
    //It can use the resource owner password mode of oidc for authentication, or others way.
    //For now we use Volo.Abp.Identity.Domain for authentication.
    public interface IEzGetUserAuthenticator
    {
        Task<EzGetUserAuthenticateionResult> AuthenticateAsync(string userName, string password);
    }
}
