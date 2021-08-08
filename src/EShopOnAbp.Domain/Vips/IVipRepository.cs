using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace EShopOnAbp.Vips
{
    public interface IVipRepository : IRepository<Vip, string>,IQueryable
    {
        Task<List<string>> GetVipIdsFromRecordsAsync(ISpecification<VipScoreRecord> specification);
    }
}