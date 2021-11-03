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

            var nuGetPackage = ezGetPulicGroup.AddPermission(EzGetPublicPermissions.NuGetPackages.Default, L("Permission:NuGetPakcagesPublic"));
            nuGetPackage.AddChild(EzGetPublicPermissions.NuGetPackages.Create, L("Permission:NuGetPakcages.Create"));
            nuGetPackage.AddChild(EzGetPublicPermissions.NuGetPackages.Unlist, L("Permission:NuGetPakcages.Unlist"));
            nuGetPackage.AddChild(EzGetPublicPermissions.NuGetPackages.Relist, L("Permission:NuGetPakcages.Relist"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EzGetResource>(name);
        }
    }
}