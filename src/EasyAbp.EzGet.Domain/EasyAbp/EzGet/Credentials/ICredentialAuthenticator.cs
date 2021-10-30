using EasyAbp.EzGet.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Credentials
{
    public interface ICredentialAuthenticator
    {
        Task<CredentialAuthenticateResult> AuthenticateAsync(string credential);
    }
}
