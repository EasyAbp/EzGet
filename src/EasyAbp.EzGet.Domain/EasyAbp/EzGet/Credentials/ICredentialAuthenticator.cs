using EasyAbp.EzGet.Users;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Credentials
{
    public interface ICredentialAuthenticator
    {
        Task<CredentialAuthenticationResult> AuthenticateAsync([NotNull]string credentialValue);
    }
}
