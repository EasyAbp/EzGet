using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EzGet.Credentials
{
    public interface ICredentialRepository : IBasicRepository<Credential, Guid>
    {
    }
}
