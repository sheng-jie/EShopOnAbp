using System;
using Volo.Abp.Domain.Entities;

namespace EShopOnAbp.Customers
{
    public class VipScoreRecord : Entity<int>
    {
        public enum RecordTypeEnum
        {
            Add,
            Reduce,
            Exchange,
            Expired
        }

        public enum RecordStatusEnum
        {
            Active,
            Used,
            Expired
        }


        public string VipId { get; private set; }

        public RecordTypeEnum RecordType { get; }

        public RecordStatusEnum RecordStatus { get; private set; }
        public int Before { get; }
        public int Changed { get; }
        public int After { get; }

        public int Left { get; set; }
        public DateTime RecordDate { get; }

        public DateTime LastUpdateDate { get; private set; }

        private VipScoreRecord()
        {
        }

        public VipScoreRecord(string vipId, RecordTypeEnum recordType, int before, int changed)
        {
            VipId = vipId;
            Before = before;
            Changed = changed;
            After = before + changed;
            RecordDate = DateTime.Now;
            RecordType = recordType;
            switch (recordType)
            {
                case RecordTypeEnum.Add:
                    RecordStatus = RecordStatusEnum.Active;
                    Left = changed;
                    break;
                case RecordTypeEnum.Exchange:
                case RecordTypeEnum.Reduce:
                    RecordStatus = RecordStatusEnum.Used;
                    Left = 0;
                    break;
                case RecordTypeEnum.Expired:
                    RecordStatus = RecordStatusEnum.Expired;
                    Left = 0;
                    break;
            }

            RecordDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
        }

        public int SetExpired()
        {
            if (RecordType != RecordTypeEnum.Add) return 0;

            var expiredScore = Left;
            Left = 0;
            RecordStatus = RecordStatusEnum.Expired;
            LastUpdateDate = DateTime.Now;

            return expiredScore;
        }

        //尝试进行积分扣减，返回剩余需要扣减的积分数
        public int TryReduce(int needScore)
        {
            //要求记录必须处于活动的新增类型记录
            if (RecordType != RecordTypeEnum.Add && RecordStatus != RecordStatusEnum.Active)
                return needScore;

            if (Left < needScore) return needScore;

            Left -= needScore;
            //如果扣减完毕则标记状态为已使用
            if (Left == 0) RecordStatus = RecordStatusEnum.Used;
            LastUpdateDate = DateTime.Now;
            return 0;
        }
    }
}