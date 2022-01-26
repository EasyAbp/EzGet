using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EzGet.PackageRegistrations
{
    public class PackageRegistrationUser : Entity
    {
        public Guid PackageRegistrationId { get; }
        public Guid UserId { get; }

        public override object[] GetKeys()
        {
            return new object[] { PackageRegistrationId, UserId };
        }

        private PackageRegistrationUser()
        {
        }

        public PackageRegistrationUser(Guid packageRegistrationId, Guid userId)
        {
            PackageRegistrationId = packageRegistrationId;
            UserId = userId;
        }
    }
}
