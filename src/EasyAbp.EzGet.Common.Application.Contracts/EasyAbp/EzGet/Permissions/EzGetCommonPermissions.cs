using Volo.Abp.Reflection;

namespace EasyAbp.EzGet.Permissions
{
    public class EzGetCommonPermissions
    {
        public const string GroupName = "EzGet";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(EzGetCommonPermissions));
        }
    }
}