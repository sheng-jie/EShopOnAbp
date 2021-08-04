using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EShopOnAbp.Data;
using Volo.Abp.DependencyInjection;

namespace EShopOnAbp.EntityFrameworkCore
{
    public class EntityFrameworkCoreEShopOnAbpDbSchemaMigrator
        : IEShopOnAbpDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreEShopOnAbpDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the EShopOnAbpDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<EShopOnAbpDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
