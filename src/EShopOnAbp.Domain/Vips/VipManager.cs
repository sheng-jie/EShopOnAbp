using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EShopOnAbp.Vips
{
    public class VipManager : DomainService
    {
        private readonly IRepository<Vip> _vipRepository;

        public VipManager(IRepository<Vip> vipRepository)
        {
            _vipRepository = vipRepository;
        }


        public async Task ScanExpiredScoreRecordsAsync()
        {
            var queryable = await _vipRepository.GetQueryableAsync();

            var vips = queryable.Where(t =>
                t.ScoreRecords.Any(r => new ExpiredVipScoreRecordSpecification().IsSatisfiedBy(r)));

            foreach (var vip in vips)
            {
                vip.CheckExpiredRecord();
            }
        }
    }
}