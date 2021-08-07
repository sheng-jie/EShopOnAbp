using EShopOnAbp.Vips;
using Volo.Abp.Application.Dtos;

namespace EShopOnAbp.Customers
{
    public class VipDto : EntityDto<string>
    {
        public VipLevel Level { get; set; }

        public int Score { get; set; }
    }
}