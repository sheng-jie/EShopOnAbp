using EShopOnAbp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace EShopOnAbp.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(EShopOnAbpEntityFrameworkCoreModule),
        typeof(EShopOnAbpApplicationContractsModule)
        )]
    public class EShopOnAbpDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
