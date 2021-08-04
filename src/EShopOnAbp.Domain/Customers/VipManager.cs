using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Specifications;

namespace EShopOnAbp.Customers
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

    public class ExpiredVipScoreRecordSpecification : Specification<VipScoreRecord>
    {
        public override Expression<Func<VipScoreRecord, bool>> ToExpression()
        {
            return record => record.RecordType == VipScoreRecord.RecordTypeEnum.Add &&
                             record.RecordStatus == VipScoreRecord.RecordStatusEnum.Active &&
                             record.Left > 0 &&
                             record.RecordDate <= DateTime.Now.AddYears(-1);
        }
    }
}