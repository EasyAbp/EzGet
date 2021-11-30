using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Admin.Credentials
{
    public class UpdateCredentialDto : CreateOrUpdateCredentialDto
    {
        public DateTime? Expires { get; set; }
    }
}
