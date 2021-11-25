using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EzGet.Admin.Users
{
    public class EzGetUserDto : FullAuditedEntityDto<Guid>, IMultiTenant
    {
        public virtual string UserName { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual bool EmailConfirmed { get; protected set; }
        public virtual string PhoneNumber { get; protected set; }
        public virtual bool PhoneNumberConfirmed { get; protected set; }
        public virtual Guid? TenantId { get; protected set; }
    }
}
