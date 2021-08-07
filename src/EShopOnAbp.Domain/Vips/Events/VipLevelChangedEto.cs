using System;

namespace EShopOnAbp.Vips.Events
{
    /// <summary>
    /// 会员等级变更事件
    /// </summary>
    public class VipLevelChangedEto
    {
        public string VipId { get; set; }
        public VipLevel Before { get; set; }
        public VipLevel After { get; set; }
        public DateTime EventTime { get; set; }

        public VipLevelChangedEto(string vipId, VipLevel before, VipLevel after)
        {
            VipId = vipId;
            Before = before;
            After = after;
            EventTime = DateTime.Now;
        }
    }
}