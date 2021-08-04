using System.Threading.Tasks;

namespace EShopOnAbp.Data
{
    public interface IEShopOnAbpDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
