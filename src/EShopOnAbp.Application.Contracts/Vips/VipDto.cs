using Volo.Abp.Application.Dtos;

namespace EShopOnAbp.Vips
{
    public class VipDto : EntityDto<string>
    {
        public VipLevel Level { get; set; }

        public int Score { get; set; }
    }
}