﻿using EasyAbp.EzGet.Credentials;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.Admin.Credentials
{
    public abstract class CreateOrUpdateCredentialDto : IHasConcurrencyStamp
    {
        public string Description { get; set; }
        public List<ScopeAllowActionEnum> Scopes { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
