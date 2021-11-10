using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Credentials
{
    public interface ICredentialStore
    {
        Task<CredentialCacheItem> GetAsync([NotNull] string value);
    }
}
