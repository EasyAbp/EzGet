using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Users;

namespace EasyAbp.EzGet.Users
{
    public class EzGetUserAuthenticateionResult
    {
        public bool Success { get; }
        public EzGetUser User { get; }

        public EzGetUserAuthenticateionResult(bool success, EzGetUser user)
        {
            Success = success;
            User = user;
        }
    }
}
