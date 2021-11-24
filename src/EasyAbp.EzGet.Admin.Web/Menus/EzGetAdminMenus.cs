namespace EasyAbp.EzGet.Admin.Web.Menus
{
    public class EzGetAdminMenus
    {
        public const string GroupName = "EzGetAdmin";

        //Add your menu items here...
        //public const string Home = Prefix + ".MyNewMenuItem";

        public static class NuGetPackages
        {
            public const string NuGetPackagesMenu = GroupName + ".NuGetPackages";
        }

        public static class Credentials
        {
            public const string CredentialsMenu = GroupName + ".Credentials";
        }

        public static class Feeds
        {
            public const string FeedsMenu = GroupName + ".Feeds";
        }
    }
}