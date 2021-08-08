using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace EShopOnAbp.Vips
{
    public class VipAppService : EShopOnAbpAppService, IVipAppService
    {
        private readonly VipManager _vipManager;
        private readonly IVipRepository _vipRepository;

        public VipAppService(VipManager vipManager,
            IVipRepository vipRepository)
        {
            _vipManager = vipManager;
            _vipRepository = vipRepository;
        }

        [HttpPost("/{vipId}/Score/{score}")]
        public async Task<int> AddScoreAsync(string vipId, int score)
        {
            var vip = await _vipRepository.FindAsync(t => t.Id == vipId);

            vip.AddScore(score);
            return vip.Score;
        }


        [HttpGet("/{customerId}")]
        public async Task<VipDto> GetVipByCustomerIdAsync(string customerId)
        {
            var vip = await _vipRepository.FirstOrDefaultAsync(t => t.CustomerId == customerId);
            if (vip == null)
            {
                using var uow = UnitOfWorkManager.Begin();
                vip = await _vipManager.CreateVipAsync(customerId);

                await uow.CompleteAsync();
            }

            var vipDto = ObjectMapper.Map<Vip, VipDto>(vip);

            return vipDto;
        }

        [HttpPost]
        public async Task CheckExpiredScoresAsync()
        {
            //获取包含过期记录的会员ID列表
            var vipIds = await _vipManager.GetHasExpiredRecordVipIdsAsync();

            //每100个VIP共享一个独立事务
            var splitVipIdArrays = Enumerable.Range(0, (int) Math.Ceiling(vipIds.Count / 100.00))
                .Select(x => vipIds.Skip(x * 100).Take(100).ToArray()).ToArray();

            foreach (var ids in splitVipIdArrays)
            {
                //每一批次采用独立事务
                using var uow = UnitOfWorkManager.Begin(requiresNew: true);
                //遍历调用领域服务进行过期记录标记
                foreach (var vipId in ids)
                {
                    await _vipManager.CleanVipExpiredScoreRecordsAsync(vipId);
                }
                //提交事务
                await uow.CompleteAsync();
            }
        }
    }
}