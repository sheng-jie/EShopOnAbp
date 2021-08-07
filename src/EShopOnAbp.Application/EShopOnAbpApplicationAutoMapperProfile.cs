using AutoMapper;
using EShopOnAbp.Customers;
using EShopOnAbp.Vips;

namespace EShopOnAbp
{
    public class EShopOnAbpApplicationAutoMapperProfile : Profile
    {
        public EShopOnAbpApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Vip, VipDto>();
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}