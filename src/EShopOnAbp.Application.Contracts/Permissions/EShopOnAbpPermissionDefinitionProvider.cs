using EShopOnAbp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EShopOnAbp.Permissions
{
    public class EShopOnAbpPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(EShopOnAbpPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(EShopOnAbpPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EShopOnAbpResource>(name);
        }
    }
}
