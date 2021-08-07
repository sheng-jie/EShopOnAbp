using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace EShopOnAbp.Vips
{
    public class ExpiredVipScoreRecordSpecification : Specification<VipScoreRecord>
    {
        public override Expression<Func<VipScoreRecord, bool>> ToExpression()
        {
            return record => record.VipScoreRecordType == VipScoreRecordTypeEnum.Add &&
                             record.VipScoreRecordStatus == VipScoreRecordStatusEnum.Active &&
                             record.Left > 0 &&
                             record.RecordDate <= DateTime.Now.AddYears(-1);
        }
    }
}