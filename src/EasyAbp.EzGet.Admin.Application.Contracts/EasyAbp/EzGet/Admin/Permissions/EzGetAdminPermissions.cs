using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Admin.Permissions
{
    public static class EzGetAdminPermissions
    {
        public const string GroupName = "EzGet.Admin";

        public static class NuGetPackages
        {
            public const string Default = GroupName + ".NuGetPackages";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class Credentials
        {
            public const string Default = GroupName + ".Credentials";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class Feeds
        {
            public const string Default = GroupName + ".Feeds";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class Users
        {
            public const string Default = GroupName + ".Users";
        }
    }
}
