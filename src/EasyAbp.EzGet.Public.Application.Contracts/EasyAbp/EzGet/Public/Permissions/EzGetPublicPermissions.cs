using Volo.Abp.Reflection;

namespace EasyAbp.EzGet.Public.Permissions
{
    public static class EzGetPublicPermissions
    {
        public const string GroupName = "EzGet.Public";

        public static class NuGetPackages
        {
            public const string Default = GroupName + ".NuGetPackages";
            public const string Create = Default + ".Create";
            public const string Unlist = Default + ".Unlist";
            public const string Relist = Default + ".Relist";
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

        public static class RegistrationIndexs
        {
            public const string Default = GroupName + ".RegistrationIndexs";
        }

        public static class ServiceIndexs
        {
            public const string Default = GroupName + ".ServiceIndexs";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(EzGetPublicPermissions));
        }
    }
}