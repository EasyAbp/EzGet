using EasyAbp.EzGet.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Credentials
{
    public class CredentialAuthenticateResult
    {
        public bool Success { get; }
        public EzGetUser User { get; }

        public CredentialAuthenticateResult(bool success, EzGetUser user)
        {
            Success = success;
            User = user;
        }
    }
}
