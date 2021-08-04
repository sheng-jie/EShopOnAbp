using EShopOnAbp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EShopOnAbp
{
    [DependsOn(
        typeof(EShopOnAbpEntityFrameworkCoreTestModule)
        )]
    public class EShopOnAbpDomainTestModule : AbpModule
    {

    }
}