using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EShopOnAbp.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class EShopOnAbpDbContextFactory : IDesignTimeDbContextFactory<EShopOnAbpDbContext>
    {
        public EShopOnAbpDbContext CreateDbContext(string[] args)
        {
            EShopOnAbpEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<EShopOnAbpDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new EShopOnAbpDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EShopOnAbp.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
