using System;

namespace EShopOnAbp.Customers.Events
{
    /// <summary>
    /// 会员等级变更事件
    /// </summary>
    public class VipLevelChangedEto
    {
        public string VipId { get; set; }
        public Vip.VipLevel Before { get; set; }
        public Vip.VipLevel After { get; set; }
        public DateTime EventTime { get; set; }

        public VipLevelChangedEto(string vipId, Vip.VipLevel before, Vip.VipLevel after)
        {
            VipId = vipId;
            Before = before;
            After = after;
            EventTime = DateTime.Now;
        }
    }
}