using System;
using Volo.Abp.Domain.Entities;

namespace EShopOnAbp.Vips
{
    public class VipScoreRecord : Entity<int>
    {
        
        
        public string VipId { get; private set; }

        public VipScoreRecordTypeEnum RecordType { get; private set; }

        public VipScoreRecordStatusEnum RecordStatus { get; private set; }
        public int Before { get; private set; }
        public int Changed { get; private set; }
        public int After { get; private set; }

        public int Left { get; private set; }
        public DateTime RecordDate { get; private set; }

        public DateTime LastUpdateDate { get; private set; }

        private VipScoreRecord()
        {
        }

        public VipScoreRecord(string vipId, VipScoreRecordTypeEnum recordType, int before, int changed)
        {
            VipId = vipId;
            Before = before;
            Changed = changed;
            After = before + changed;
            RecordDate = DateTime.Now;
            RecordType = recordType;
            switch (recordType)
            {
                case VipScoreRecordTypeEnum.Add:
                    RecordStatus = VipScoreRecordStatusEnum.Active;
                    Left = changed;
                    break;
                case VipScoreRecordTypeEnum.Exchange:
                case VipScoreRecordTypeEnum.Reduce:
                    RecordStatus = VipScoreRecordStatusEnum.Used;
                    Left = 0;
                    break;
                case VipScoreRecordTypeEnum.Expired:
                    RecordStatus = VipScoreRecordStatusEnum.Expired;
                    Left = 0;
                    break;
            }

            RecordDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
        }

        public int SetExpired()
        {
            if (RecordType != VipScoreRecordTypeEnum.Add) return 0;

            var expiredScore = Left;
            Left = 0;
            RecordStatus = VipScoreRecordStatusEnum.Expired;
            LastUpdateDate = DateTime.Now;

            return expiredScore;
        }

        //尝试进行积分扣减，返回剩余需要扣减的积分数
        public int TryReduce(int needScore)
        {
            //要求记录必须处于活动的新增类型记录
            if (RecordType != VipScoreRecordTypeEnum.Add && RecordStatus != VipScoreRecordStatusEnum.Active)
                return needScore;

            if (Left < needScore) return needScore;

            Left -= needScore;
            //如果扣减完毕则标记状态为已使用
            if (Left == 0) RecordStatus = VipScoreRecordStatusEnum.Used;
            LastUpdateDate = DateTime.Now;
            return 0;
        }
    }
}