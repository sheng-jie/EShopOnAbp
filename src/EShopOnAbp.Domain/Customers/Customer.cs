using Volo.Abp.Domain.Entities;

namespace EShopOnAbp.Customers
{
    public class Customer : AggregateRoot<string>
    {
        public string Phone { get; set; }

        public string NicName { get; set; }
    }
}