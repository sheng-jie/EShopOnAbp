using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace EShopOnAbp.Web
{
    [Dependency(ReplaceServices = true)]
    public class EShopOnAbpBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "EShopOnAbp";
    }
}
