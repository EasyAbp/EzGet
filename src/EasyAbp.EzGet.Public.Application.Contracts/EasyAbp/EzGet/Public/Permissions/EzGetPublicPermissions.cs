using Volo.Abp.Reflection;

namespace EasyAbp.EzGet.Public.Permissions
{
    public class EzGetPublicPermissions
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

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(EzGetPublicPermissions));
        }
    }
}