using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EzGet
{
    [Dependency(ReplaceServices = true)]
    public class EzGetBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "EzGet";
    }
}
