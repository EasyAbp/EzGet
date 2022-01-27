using System;
using EasyAbp.EzGet.PackageRegistrations;

namespace EasyAbp.EzGet.Public.PackageRegistrations
{
    public class AddOwnerDto
    {
        public Guid UserId { get; set; }
        public PackageRegistrationOwnerTypeEnum OwnerType { get; set; }
    }
}

