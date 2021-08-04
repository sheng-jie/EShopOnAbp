using EShopOnAbp.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EShopOnAbp.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class EShopOnAbpPageModel : AbpPageModel
    {
        protected EShopOnAbpPageModel()
        {
            LocalizationResourceType = typeof(EShopOnAbpResource);
        }
    }
}