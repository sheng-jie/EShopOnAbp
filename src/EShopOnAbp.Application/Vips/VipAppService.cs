using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace EShopOnAbp.Vips
{
    public class VipAppService : EShopOnAbpAppService, IVipAppService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly VipManager _vipManager;
        private readonly IRepository<Vip,string> _vipRepository;

        public VipAppService(IUnitOfWorkManager unitOfWorkManager, VipManager vipManager,
            IRepository<Vip, string> vipRepository)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _vipManager = vipManager;
            _vipRepository = vipRepository;
        }

        [HttpPost("{vipId}/Score/{score}")]
        public async Task AddScoreAsync(string vipId, int score)
        {
            var vip = await _vipRepository.FindAsync(t => t.Id == vipId);

            vip.AddScore(score);
        }


        [HttpGet("{customerId}")]
        public async Task<VipDto> GetVipByCustomerIdAsync(string customerId)
        {
            var vip = await _vipRepository.FindAsync(t => t.CustomerId == customerId);
            if (vip == null)
            {
                using var uow = _unitOfWorkManager.Begin();
                vip = await _vipManager.CreateVipAsync(customerId);

                await uow.CompleteAsync();
            }

            var vipDto = ObjectMapper.Map<Vip, VipDto>(vip);

            return vipDto;
        }
    }
}