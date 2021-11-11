using EasyAbp.EzGet.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EzGet.Public.Permissions
{
    public class EzGetPublicPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var ezGetPulicGroup = context.AddGroup(EzGetPublicPermissions.GroupName, L("Permission:EzGetPublic"));

            DefineNuGetPackages(ezGetPulicGroup);
            DefineCredentials(ezGetPulicGroup);
            DefineFeeds(ezGetPulicGroup);
        }

        private void DefineNuGetPackages(PermissionGroupDefinition ezGetPulicGroup)
        {
            var nuGetPackage = ezGetPulicGroup.AddPermission(EzGetPublicPermissions.NuGetPackages.Default, L("Permission:NuGetPakcagesPublic"));
            nuGetPackage.AddChild(EzGetPublicPermissions.NuGetPackages.Create, L("Permission:NuGetPakcages.Create"));
            nuGetPackage.AddChild(EzGetPublicPermissions.NuGetPackages.Unlist, L("Permission:NuGetPakcages.Unlist"));
            nuGetPackage.AddChild(EzGetPublicPermissions.NuGetPackages.Relist, L("Permission:NuGetPakcages.Relist"));
        }

        private void DefineCredentials(PermissionGroupDefinition ezGetPulicGroup)
        {
            var credential = ezGetPulicGroup.AddPermission(EzGetPublicPermissions.Credentials.Default, L("Permission:CredentialsPublic"));
            credential.AddChild(EzGetPublicPermissions.Credentials.Create, L("Permission:Credentials.Create"));
            credential.AddChild(EzGetPublicPermissions.Credentials.Update, L("Permission:Credentials.Update"));
            credential.AddChild(EzGetPublicPermissions.Credentials.Delete, L("Permission:Credentials.Delete"));
        }

        private void DefineFeeds(PermissionGroupDefinition ezGetAdminGroup)
        {
            var feed = ezGetAdminGroup.AddPermission(EzGetPublicPermissions.Feeds.Default, L("Permission:FeedsPublic"));
            feed.AddChild(EzGetPublicPermissions.Feeds.Create, L("Permission:Feeds.Create"));
            feed.AddChild(EzGetPublicPermissions.Feeds.Update, L("Permission:Feeds.Update"));
            feed.AddChild(EzGetPublicPermissions.Feeds.Delete, L("Permission:Feeds.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EzGetResource>(name);
        }
    }
}