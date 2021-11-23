using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Admin.Credentials
{
    public abstract class CreateOrUpdateCredentialDto
    {
        public string Description { get; set; }
    }
}
