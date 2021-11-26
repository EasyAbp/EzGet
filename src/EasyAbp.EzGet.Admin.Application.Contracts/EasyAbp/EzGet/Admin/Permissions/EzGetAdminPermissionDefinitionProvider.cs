using EasyAbp.EzGet.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EzGet.Admin.Permissions
{
    public class EzGetAdminPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var ezGetAdminGroup = context.AddGroup(EzGetAdminPermissions.GroupName, L("Permission:EzGetAdmin"));

            DefineNuGetPackages(ezGetAdminGroup);
            DefineCredentials(ezGetAdminGroup);
            DefineFeeds(ezGetAdminGroup);
            DefineEzGetUsers(ezGetAdminGroup);
        }

        private void DefineNuGetPackages(PermissionGroupDefinition ezGetAdminGroup)
        {
            var nuGetPackage = ezGetAdminGroup.AddPermission(EzGetAdminPermissions.NuGetPackages.Default, L("Permission:NuGetPakcagesAdmin"));
            nuGetPackage.AddChild(EzGetAdminPermissions.NuGetPackages.Create, L("Permission:NuGetPakcages.Create"));
            nuGetPackage.AddChild(EzGetAdminPermissions.NuGetPackages.Update, L("Permission:NuGetPakcages.Update"));
            nuGetPackage.AddChild(EzGetAdminPermissions.NuGetPackages.Delete, L("Permission:NuGetPakcages.Delete"));
        }

        private void DefineCredentials(PermissionGroupDefinition ezGetAdminGroup)
        {
            var credential = ezGetAdminGroup.AddPermission(EzGetAdminPermissions.Credentials.Default, L("Permission:CredentialsAdmin"));
            credential.AddChild(EzGetAdminPermissions.Credentials.Create, L("Permission:Credentials.Create"));
            credential.AddChild(EzGetAdminPermissions.Credentials.Update, L("Permission:Credentials.Update"));
            credential.AddChild(EzGetAdminPermissions.Credentials.Delete, L("Permission:Credentials.Delete"));
        }

        private void DefineFeeds(PermissionGroupDefinition ezGetAdminGroup)
        {
            var feed = ezGetAdminGroup.AddPermission(EzGetAdminPermissions.Feeds.Default, L("Permission:FeedsAdmin"));
            feed.AddChild(EzGetAdminPermissions.Feeds.Create, L("Permission:Feeds.Create"));
            feed.AddChild(EzGetAdminPermissions.Feeds.Update, L("Permission:Feeds.Update"));
            feed.AddChild(EzGetAdminPermissions.Feeds.Delete, L("Permission:Feeds.Delete"));
        }

        private void DefineEzGetUsers(PermissionGroupDefinition ezGetAdminGroup)
        {
            ezGetAdminGroup.AddPermission(EzGetAdminPermissions.Users.Default, L("Permission:EzGetUsersAdmin"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EzGetResource>(name);
        }
    }
}
