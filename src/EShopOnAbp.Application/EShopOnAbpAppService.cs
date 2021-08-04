using System;
using System.Collections.Generic;
using System.Text;
using EShopOnAbp.Localization;
using Volo.Abp.Application.Services;

namespace EShopOnAbp
{
    /* Inherit your application services from this class.
     */
    public abstract class EShopOnAbpAppService : ApplicationService
    {
        protected EShopOnAbpAppService()
        {
            LocalizationResource = typeof(EShopOnAbpResource);
        }
    }
}
