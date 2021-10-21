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
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(EzGetPublicPermissions));
        }
    }
}