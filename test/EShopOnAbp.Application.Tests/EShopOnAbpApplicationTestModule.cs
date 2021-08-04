using Volo.Abp.Modularity;

namespace EShopOnAbp
{
    [DependsOn(
        typeof(EShopOnAbpApplicationModule),
        typeof(EShopOnAbpDomainTestModule)
        )]
    public class EShopOnAbpApplicationTestModule : AbpModule
    {

    }
}