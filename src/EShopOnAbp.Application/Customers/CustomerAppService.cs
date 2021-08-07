using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EShopOnAbp.Customers
{
    public class CustomerAppService :
        CrudAppService<Customer, CustomerDto, string>,
        ICustomerAppService
    {
        public CustomerAppService(IRepository<Customer, string> repository) : base(repository)
        {
        }
    }
}