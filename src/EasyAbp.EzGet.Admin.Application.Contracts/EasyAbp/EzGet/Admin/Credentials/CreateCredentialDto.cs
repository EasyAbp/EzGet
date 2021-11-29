using EasyAbp.EzGet.Credentials;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Admin.Credentials
{
    public class CreateCredentialDto : CreateOrUpdateCredentialDto
    {
        public Guid UserId { get; set; }
        public TimeSpan? Expiration { get; set; }
        public string GlobPattern { get; set; }
    }
}
