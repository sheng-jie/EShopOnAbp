using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace EShopOnAbp.Vips
{
    public class VipManager : DomainService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IRepository<Vip> _vipRepository;

        public VipManager(IGuidGenerator guidGenerator, IRepository<Vip, string> vipRepository)
        {
            _guidGenerator = guidGenerator;
            _vipRepository = vipRepository;
        }

        public async Task<Vip> CreateVipAsync(string customerId)
        {
            var vip = await _vipRepository.FirstOrDefaultAsync(t => t.CustomerId == customerId);

            if (vip != null) return vip;

            vip = new Vip(_guidGenerator.Create().ToString(), customerId);

            await _vipRepository.InsertAsync(vip);

            return vip;
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