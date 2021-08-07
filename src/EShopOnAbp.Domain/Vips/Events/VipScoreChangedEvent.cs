using System;

namespace EShopOnAbp.Vips.Events
{
    /// <summary>
    /// 会员积分变更事件
    /// </summary>
    public class VipScoreChangedEvent
    {
        public string VipId { get; set; }
        public int Before { get; set; }
        public int After { get; set; }
        public DateTime EventTime { get; set; }

        public VipScoreChangedEvent(string vipId, int before, int after)
        {
            VipId = vipId;
            Before = before;
            After = after;
            EventTime = DateTime.Now;
        }
    }
}