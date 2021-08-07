using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EShopOnAbp.Customers
{
    public class CustomerDto : EntityDto<string>
    {
        public string Phone { get; set; }

        public string NicName { get; set; }
    }
    
    public class CreateAndUpdateCustomerDto
    {
        [Required]
        [MaxLength(16)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(64)]
        public string NicName { get; set; }
    }
}