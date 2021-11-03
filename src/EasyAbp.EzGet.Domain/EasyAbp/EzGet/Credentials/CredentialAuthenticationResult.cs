using EasyAbp.EzGet.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Credentials
{
    public class CredentialAuthenticationResult
    {
        public bool Success { get; }
        public EzGetUser User { get; }

        public CredentialAuthenticationResult(bool success, EzGetUser user)
        {
            Success = success;
            User = user;
        }
    }
}
