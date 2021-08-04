using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EShopOnAbp.Data
{
    /* This is used if database provider does't define
     * IEShopOnAbpDbSchemaMigrator implementation.
     */
    public class NullEShopOnAbpDbSchemaMigrator : IEShopOnAbpDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}