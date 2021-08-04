using Volo.Abp.Settings;

namespace EShopOnAbp.Settings
{
    public class EShopOnAbpSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(EShopOnAbpSettings.MySetting1));
        }
    }
}
