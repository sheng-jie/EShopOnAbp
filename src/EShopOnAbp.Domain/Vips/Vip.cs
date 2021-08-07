using System.Collections.Generic;
using System.Linq;
using EShopOnAbp.Vips.Events;
using Volo.Abp.Domain.Entities;

namespace EShopOnAbp.Vips
{
    public class Vip : AggregateRoot<string>
    {
        public string CustomerId { get; private set; }
        public int Score { get; private set; }

        public VipLevel Level
        {
            get
            {
                return (Score) switch
                {
                    (>4000) => VipLevel.L4,
                    (>3000) => VipLevel.L3,
                    (>2000) => VipLevel.L2,
                    (>1000) => VipLevel.L1,
                    _ => VipLevel.L0
                };
            }
        }

        public virtual List<VipScoreRecord> ScoreRecords { get; private set; }

        private Vip()
        {
            ScoreRecords = new List<VipScoreRecord>();
        }

        public Vip(string vipId, string customerId)
        {
            Id = vipId;
            CustomerId = customerId;
            ScoreRecords = new List<VipScoreRecord>();
        }

        // 增加积分
        public void AddScore(int score)
        {
            if (score <= 0) throw new InvalidScoreException(score);
            var needAddedScore = Level switch
            {
                VipLevel.L1 or VipLevel.L2 => score * 2,
                VipLevel.L3 or VipLevel.L4 => score * 3,
                _ => score * 1
            };

            AddRecord(VipScoreRecordTypeEnum.Add, needAddedScore);
        }

        // 扣减积分
        public bool ReduceScore(int score)
        {
            if (score <= 0) throw new InvalidScoreException(score);

            var isMatched = TryMatchRecord(score);
            if (isMatched)
                AddRecord(VipScoreRecordTypeEnum.Reduce, -score);

            return isMatched;
        }

        private VipScoreRecord AddRecord(VipScoreRecordTypeEnum vipScoreRecordTypeEnum, int changed)
        {
            var beforeLevel = Level;
            var newRecord = new VipScoreRecord(Id, vipScoreRecordTypeEnum, Score, changed);
            ScoreRecords.Add(newRecord);
            Score = newRecord.After;
            var afterLevel = Level;

            //若积分更新前后，等级变更，则添加等级变更事件
            if (beforeLevel != afterLevel)
            {
                AddDistributedEvent(new VipLevelChangedEto(Id, beforeLevel, afterLevel));
            }

            //添加积分变更事件
            AddDistributedEvent(new VipScoreChangedEvent(Id, newRecord.Before, newRecord.After));

            return newRecord;
        }

        // 积分兑换
        public bool ExchangeScore(int score)
        {
            if (score <= 0) throw new InvalidScoreException(score);

            var isMatched = TryMatchRecord(score);
            if (isMatched)
                AddRecord(VipScoreRecordTypeEnum.Exchange, -score);

            return isMatched;
        }

        private bool TryMatchRecord(int needScore)
        {
            if (this.Score < needScore) return false;

            var activeAddedRecords =
                ScoreRecords.Where(r =>
                    r.RecordStatus == VipScoreRecordStatusEnum.Active &&
                    r.RecordType == VipScoreRecordTypeEnum.Add).OrderBy(r => r.RecordDate);

            foreach (var scoreRecord in activeAddedRecords)
            {
                needScore = scoreRecord.TryReduce(needScore);
                if (needScore == 0)
                    break;
            }

            return true;
        }

        // 过期检查
        public void CheckExpiredRecord()
        {
            var expiredRecords =
                ScoreRecords.Where(r => new ExpiredVipScoreRecordSpecification().IsSatisfiedBy(r)).ToList();

            if (!expiredRecords.Any()) return;

            var expiredScores = 0;
            foreach (var historyRecord in expiredRecords)
            {
                //将历史记录置为过期
                var expiredScore = historyRecord.SetExpired();
                expiredScores += expiredScore;
            }

            //添加一条过期记录
            AddRecord(VipScoreRecordTypeEnum.Expired, -expiredScores);
        }
    }
}