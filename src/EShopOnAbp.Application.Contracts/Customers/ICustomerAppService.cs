using Volo.Abp.Application.Services;

namespace EShopOnAbp.Customers
{
    public interface ICustomerAppService : ICrudAppService<CustomerDto, string>
    {
        
    }
}