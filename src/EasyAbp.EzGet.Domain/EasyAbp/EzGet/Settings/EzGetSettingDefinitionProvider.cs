using EasyAbp.EzGet.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace EasyAbp.EzGet.Settings
{
    public class EzGetSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from EzGetSettings class.
             */
            context.Add(
                new SettingDefinition(
                    EzGetSettingNames.HostUrl,
                    "http://localhost:5000",
                    L("DisplayName:EasyAbp.EzGet.HostUrl"),
                    L("Description:EasyAbp.EzGet.HostUrl"))
                );
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EzGetResource>(name);
        }
    }
}