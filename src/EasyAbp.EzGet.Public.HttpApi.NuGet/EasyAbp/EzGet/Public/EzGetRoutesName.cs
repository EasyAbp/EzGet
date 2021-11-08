namespace EasyAbp.EzGet.Public
{
    public static class EzGetRoutesName
    {
        public const string Feed = "feed-";

        public static class PackagePublish
        {
            public const string Create = "package-create";
            public const string Unlist = "packakge-unlist";
            public const string Relist = "packakge-relist";
        }

        public static class PackageContent
        {
            public const string GetVersions = "get-package-versions";
            public const string GetPackageContent = "get-package-content";
            public const string GetPackageManifest = "get-package-manifest";
        }

        public static class PackageSearch
        {
            public const string GetList = "get-list";
        }

        public static class RegistrationIndex
        {
            public const string GetIndex = "get-registration-index";
            public const string GetLeaf = "get-registration-leaf";
        }

        public static class ServiceIndex
        {
            public const string Get = "get-service-index";
        }
    }
}
